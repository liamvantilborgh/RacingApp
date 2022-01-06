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
    public class RacesController : Controller
    {
        private WebClient _client;
        public RacesController()
        {
            GetWebClient();
        }

        public IActionResult Index()
        {
            string json = _client.DownloadString("races");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<RacesDTO>>(json);
            result = result.OrderBy(r => r.Startdate);
            return View("Index", result);
        }

        public IActionResult Create()
        {
            var json = _client.DownloadString("seasons");
            var seasons = new JavaScriptSerializer().Deserialize<IEnumerable<SeasonsDTO>>(json);
            var json2 = _client.DownloadString("circuits");
            var circuits = new JavaScriptSerializer().Deserialize<IEnumerable<CircuitsDTO>>(json2);


            RaceSeasonCircuit raceSeasonCircuit = new((List<SeasonsDTO>)seasons, (List<CircuitsDTO>) circuits);
            return View(raceSeasonCircuit);
        }

        public IActionResult Add(RaceSeasonCircuit raceSeasonCircuit)
        {
            RacesDTO raceToAdd = raceSeasonCircuit.Race;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            raceToAdd.SeasonId = int.Parse(raceSeasonCircuit.SeasonId);
            raceToAdd.CircuitId = int.Parse(raceSeasonCircuit.CircuitId);
            var result = _client.UploadString("races/add", JsonConvert.SerializeObject(raceToAdd));

            if (result.Length > 0)
            {
                return Redirect("Index");
            }
            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("races/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("races/" + id);
            var race = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json);

            var json2 = _client.DownloadString("seasons");
            var seasons = new JavaScriptSerializer().Deserialize<IEnumerable<SeasonsDTO>>(json2);

            var json3 = _client.DownloadString("circuits");
            var circuits = new JavaScriptSerializer().Deserialize<IEnumerable<CircuitsDTO>>(json3);

            RaceSeasonCircuit raceSeasonCircuit = new((List<SeasonsDTO>)seasons, (List<CircuitsDTO>)circuits, race);
            return View("Edit", raceSeasonCircuit);
        }

        public IActionResult EditRace(int Id, RaceSeasonCircuit raceSeasonCircuit)
        {
            RacesDTO raceToEdit = raceSeasonCircuit.Race;
            //need to acces an internal property from custom class otherwise it tries to put a string id in a int prop
            raceToEdit.SeasonId = int.Parse(raceSeasonCircuit.SeasonId);
            raceToEdit.CircuitId = int.Parse(raceSeasonCircuit.CircuitId);
            var result = _client.UploadString("races/update/" + Id, JsonConvert.SerializeObject(raceToEdit));

            if (result.Length > 0)
            {
                return Redirect("Index");
            }
            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("races/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json);

            return View("Delete", result);
        }

        public IActionResult DeleteRace(int id)
        {
            _client.UploadString("races/delete/" + id, "POST", "");
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
