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
    public class SeasonsController : Controller
    {
        private WebClient _client;
        public SeasonsController()
        {
            GetWebClient();
        }

        public IActionResult Index()
        {
            string json = _client.DownloadString("seasons");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<SeasonsDTO>>(json);
            result = result.OrderBy(r => r.Startdate);
            return View("Index", result);
        }

        public IActionResult Create()
        {
            var json = _client.DownloadString("series");
            var series = new JavaScriptSerializer().Deserialize<IEnumerable<SeriesDTO>>(json);


            SeasonsSerie seasonSerie = new((List<SeriesDTO>)series);
            return View(seasonSerie);
        }

        public IActionResult Add(SeasonsSerie seasonSerie)
        {
            SeasonsDTO seasonsToAdd = seasonSerie.Season;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            seasonsToAdd.SeriesId = int.Parse(seasonSerie.SeriesId);
            try
            {
                var result = _client.UploadString("seasons/add", JsonConvert.SerializeObject(seasonsToAdd));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the season, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("seasons/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeasonsDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("seasons/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeasonsDTO>(json);

            var json2 = _client.DownloadString("series");
            var series = new JavaScriptSerializer().Deserialize<IEnumerable<SeriesDTO>>(json2);

            SeasonsSerie seasonsSerie = new((List<SeriesDTO>)series, result);
            return View("Edit", seasonsSerie);

        }

        public IActionResult EditSeason(int Id, SeasonsSerie seasonSerie)
        {
            SeasonsDTO seasonToEdit = seasonSerie.Season;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            seasonToEdit.SeriesId = int.Parse(seasonSerie.SeriesId);
            try
            {
                var result = _client.UploadString("seasons/update/" + Id, JsonConvert.SerializeObject(seasonToEdit));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the season, try a different name or try again.");
                return View("Exception", Exception);
            }
            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("seasons/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<SeasonsDTO>(json);

            return View("Delete", result);

        }

        public IActionResult DeleteSeason(int id)
        {
            try
            {
                _client.UploadString("seasons/delete/" + id, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the season, please make sure no race is connected to this season.");
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
