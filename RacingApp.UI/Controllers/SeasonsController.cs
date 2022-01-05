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
            SeasonsDTO seaonsToAdd = seasonSerie.Season;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            seaonsToAdd.SeriesId = int.Parse(seasonSerie.SeriesId);
            var result = _client.UploadString("seasons/add", JsonConvert.SerializeObject(seaonsToAdd));

            if (result.Length > 0)
            {
                return Redirect("Index");
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
            var result = _client.UploadString("seasons/update/" + Id, JsonConvert.SerializeObject(seasonToEdit));

            if (result.Length > 0)
            {
                return Redirect("Index");
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
            _client.UploadString("seasons/delete/" + id, "POST", "");
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
