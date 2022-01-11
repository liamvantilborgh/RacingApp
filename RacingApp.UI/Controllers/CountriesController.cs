using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RacingApp.Core.DTO_S;
using RacingApp.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.UI.Controllers
{
    public class CountriesController : Controller
    {
        private WebClient _client;
        public CountriesController()
        {
            GetWebClient();
        }

        public IActionResult Index(int? pageNumber, int currentSize, int? customSize)
        {
            ViewData["CurrentPageSize"] = customSize;

            //to make sure the standard pagesize is 50
            int pageSize = 50;
            if (currentSize == 0)
            {
                currentSize = pageSize;
            }

            //remembers custom page size when switch pages
            if (customSize != null)
            {
                pageNumber = 1;
            }
            else
            {
                customSize = currentSize;
            }

            ViewData["Currentsize"] = customSize;

            if (customSize.HasValue)
            {
                pageSize = (int)customSize;
            }

            string json = _client.DownloadString("countries");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<CountryDTO>>(json);
            return View("Index", PaginatedList<CountryDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(CountryDTO country)
        {
            try
            {
                string data = JsonConvert.SerializeObject(country);
                var result = _client.UploadString("countries/add", data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the country, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString(_client.BaseAddress + "countries/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CountryDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString(_client.BaseAddress + "countries/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CountryDTO>(json);

            return View("Edit", result);

        }

        public IActionResult EditCountry(int Id, CountryDTO country)
        {
            try
            {
                string data = JsonConvert.SerializeObject(country);
                var result = _client.UploadString(_client.BaseAddress + "countries/update/" + Id, data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the country, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString(_client.BaseAddress + "countries/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CountryDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                _client.UploadString(_client.BaseAddress + "countries/delete/" + id, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the country, make sure no circuit is connected to this country.");
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
