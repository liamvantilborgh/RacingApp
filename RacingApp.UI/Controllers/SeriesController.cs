using Abp.UI;
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

namespace RacingApp.UI.Controllers
{
    public class SeriesController : Controller
    {
        private WebClient _client;
        public SeriesController()
        {
            GetWebClient();
        }

        public IActionResult Index(string searchString, string currentFilter, int? pageNumber, int currentSize, int? customSize)
        {
            //so it knows what i'm trying to filter 
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentPageSize"] = customSize;

            //to make sure the standard pagesize is 50
            int pageSize = 50;
            if (currentSize == 0)
            {
                currentSize = pageSize;
            }

            //get all the series
            string json = _client.DownloadString("series");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<SeriesDTO>>(json);

            //update active everytime list is called
            foreach (var s in result)
            {
                if (s.Startdate > DateTime.Today || s.Enddate < DateTime.Today)
                {
                    s.Active = false;
                }
                else
                {
                    s.Active = true;
                }
            }

            //reset page number to 1 when you filter a row
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (customSize != null)
            {
                pageSize = 1;
            }
            else
            {
                customSize = currentSize;
            }

            ViewData["Currentsize"] = customSize;

            //the actual filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            if (customSize.HasValue)
            {
                pageSize = (int)customSize;
            }

            //sorting
            result = result.OrderBy(r => r.Sort_Order).ThenBy(r => r.Name);

            return View("Index", PaginatedList<SeriesDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Add(SeriesDTO serie)
        {
            if (serie.Startdate > DateTime.Today || serie.Enddate < DateTime.Today)
            {
                serie.Active = false;
            }
            else //if(seasonSerie.Season.Enddate.Equals(null))
            {
                serie.Active = true;
            }

            try
            {
                string data = JsonConvert.SerializeObject(serie);
                var result = _client.UploadString("series/add", data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when adding the series, try a different name or try again.");
                return View("Exception", Exception); 
                //return View("Create");
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
            if(serie.Startdate > DateTime.Today || serie.Enddate < DateTime.Today)
            {
                serie.Active = false;
            }
            else //if(seasonSerie.Season.Enddate.Equals(null))
            {
                serie.Active = true;
            }

            try
            {
                string data = JsonConvert.SerializeObject(serie);
                var result = _client.UploadString("series/update/" + Id, data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the series, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("series/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeriesDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteSeries(int id)
        {
            try
            {
                _client.UploadString("series/delete/" + id, "POST", "");
                return Redirect("Index");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the series, make sure no season is connected to this series.");
                return View("Exception", Exception);
            }
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
