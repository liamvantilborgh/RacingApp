﻿using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RacingApp.Core.DTO_S;
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

        public IActionResult Index()
        {
            string json = _client.DownloadString("countries");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<CountryDTO>>(json);
            return View("Index", result);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(CountryDTO country)
        {
            string data = JsonConvert.SerializeObject(country);
            var result = _client.UploadString("countries/add", data);

            if (result.Length > 0)
            {
                return Redirect("Index");
            }

            return null;
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
            string data = JsonConvert.SerializeObject(country);
            var result = _client.UploadString(_client.BaseAddress + "countries/update/" + Id, data);

            if (result.Length > 0)
            {
                return Redirect("Index");
            }

            return null;

        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString(_client.BaseAddress + "countries/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<CountryDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteCountry(int id)
        {
            _client.UploadString(_client.BaseAddress + "countries/delete/" + id, "POST", "");
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
