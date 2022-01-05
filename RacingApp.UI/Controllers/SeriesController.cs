﻿using Microsoft.AspNetCore.Mvc;
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
    public class SeriesController : Controller
    {
        private WebClient _client;
        public SeriesController()
        {
            GetWebClient();
        }

        public IActionResult Index()
        {
            string json = _client.DownloadString("series");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<SeriesDTO>>(json);
            //update active everytime list is called
            foreach (var s in result)
            {
                if (s.Enddate < DateTime.Today || s.Enddate.Equals(null))
                {
                    s.Active = false;
                }
                else
                {
                    s.Active = true;
                }
            }
            result = result.OrderBy(r => r.Sort_Order).ThenBy(r => r.Name);
            return View("Index", result);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Add(SeriesDTO serie)
        {
            if(serie.Enddate < DateTime.Today || serie.Enddate.Equals(null))
            {
                serie.Active = false;
            }
            else
            {
                serie.Active = true;
            }
            try
            {
                string data = JsonConvert.SerializeObject(serie);
                var result = _client.UploadString("series/add", data);
            }
            catch (Exception E)
            {
                ExceptionModel Exception = new ExceptionModel(E);
                return View("Exception", Exception);
            }


            return Redirect("Index");
        }
        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("series/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeriesDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("series/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeriesDTO>(json);

            return View("Edit", result);
        }
        public IActionResult EditSeries(int Id, SeriesDTO serie)
        {
            if (serie.Enddate < DateTime.Today || serie.Enddate.Equals(null))
            {
                serie.Active = false;
            }
            else
            {
                serie.Active = true;
            }
            string data = JsonConvert.SerializeObject(serie);
            var result = _client.UploadString("series/update/" + Id, data);

            if (result.Length > 0)
            {
                return Redirect("Index");
            }

            return null;
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("series/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeriesDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteSeries(int id)
        {
            _client.UploadString("series/delete/" + id, "POST", "");
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
