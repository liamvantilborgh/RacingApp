using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RacingApp.Core.DTO_S;
using RacingApp.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.UI.Controllers
{
    public class CircuitsController : Controller
    {
        private WebClient _client;
        public CircuitsController()
        {
            GetWebClient();
        }

        public IActionResult Index(string searchStringCountry, string searchStringName, int? pageNumber)
        {
            //so it knows what i'm trying to filter 
            ViewData["CurrentFilterCountry"] = searchStringCountry;
            ViewData["CurrentFilterName"] = searchStringName;

            //get all the circuits
            string json = _client.DownloadString("circuits");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<CircuitsDTO>>(json);

            //reset page number to 1 when you filter a row
            if (searchStringName != null || searchStringCountry != null)
            {
                pageNumber = 1;
            }

            //the actual filtering
            if (!String.IsNullOrEmpty(searchStringName))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchStringName.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchStringCountry))
            {
                result = result.Where(s => s.Country.Name.ToLower().Contains(searchStringCountry.ToLower()));
            }

            result = result.OrderBy(r => r.Country.Name).ThenBy(r => r.Name);
            //here you can edit the amount of results per page
            int pageSize = 50;
            return View("Index", PaginatedList<CircuitsDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            var json = _client.DownloadString("countries");
            var countries = new JavaScriptSerializer().Deserialize<IEnumerable<CountryDTO>>(json);


            CircuitCountry circuitCountry = new((List<CountryDTO>)countries);
            return View(circuitCountry);
        }

        public IActionResult Add(CircuitCountry circuitCountry)
        {
            CircuitsDTO circuitToAdd = circuitCountry.Circuit;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            circuitToAdd.CountryId = int.Parse(circuitCountry.CountryId);
            if (circuitCountry.miles)
            {
                var km = Math.Round(circuitCountry.LengthMiles * 1.609344);
                circuitToAdd.Length_Circuit = (int)km;
            }

            try
            {
                var result = _client.UploadString("circuits/add", JsonConvert.SerializeObject(circuitToAdd));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the circuit, try a different name or try again.");
                return View("Exception", Exception);
            }       
            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("circuits/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CircuitsDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("circuits/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CircuitsDTO>(json);

            var json2 = _client.DownloadString("countries");
            var countries = new JavaScriptSerializer().Deserialize<IEnumerable<CountryDTO>>(json2);

            CircuitCountry circuitCountry = new((List<CountryDTO>)countries, result);
            return View("Edit", circuitCountry);

        }

        public IActionResult EditCircuit(int Id, CircuitCountry circuitCountry)
        {
            CircuitsDTO circuitToEdit = circuitCountry.Circuit;
            if(circuitCountry.miles)
            {
                var km = Math.Round(circuitCountry.LengthMiles * 1.609344);
                circuitToEdit.Length_Circuit = (int)km;
            }
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            circuitToEdit.CountryId = int.Parse(circuitCountry.CountryId);

            try
            {
                var result = _client.UploadString("circuits/update/" + Id, JsonConvert.SerializeObject(circuitToEdit));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the circuit, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public void GetWebClient()
        {
            _client = new();
            _client.Headers["Content-type"] = "application/json";
            _client.Encoding = Encoding.UTF8;
            _client.BaseAddress = "https://localhost:44334/api/";
        }
    }
}
