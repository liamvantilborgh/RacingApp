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

        public IActionResult Index()
        {
            string json = _client.DownloadString("circuits");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<CircuitsDTO>>(json);
            result = result.OrderBy(r => r.Country.Name);
            return View("Index", result);
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
