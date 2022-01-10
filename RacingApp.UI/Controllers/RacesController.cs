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
    public class RacesController : Controller
    {
        private WebClient _client;
        public RacesController()
        {
            GetWebClient();
        }

        public IActionResult Index(string searchStringSeason, string searchStringName, int? pageNumber)
        {
            //so it knows what i'm trying to filter 
            ViewData["CurrentFilterSeason"] = searchStringSeason;
            ViewData["CurrentFilterName"] = searchStringName;

            //get all the races
            string json = _client.DownloadString("races");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<RacesDTO>>(json);

            //reset page number to 1 when you filter a row
            if (searchStringName != null || searchStringSeason != null)
            {
                pageNumber = 1;
            }

            //the actual filtering
            if (!String.IsNullOrEmpty(searchStringSeason))
            {
                result = result.Where(s => s.Season.Name.ToLower().Contains(searchStringSeason.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchStringName))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchStringName.ToLower()));
            }
            
            result = result.OrderBy(r => r.Startdate);

            //here you can edit the amount of results per page
            int pageSize = 50;
            return View("Index", PaginatedList<RacesDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
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

            try
            {
                var result = _client.UploadString("races/add", JsonConvert.SerializeObject(raceToAdd));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the race, try a different name and make sure the dates of the race are in between the dates of the season or try again.");
                return View("Exception", Exception);
            }

            return Redirect("Index");
        }

        public IActionResult Details(int id, string searchString, int? pageNumber)
        {
            string json = _client.DownloadString("races/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json);

            //so it knows what i'm trying to filter, here I filter on teams that are connected to a certain race
            ViewData["CurrentFilter"] = searchString;

            string json2 = _client.DownloadString("pilotraceteam/GetTeamsByRaceId/" + id);
            var pilotRaceTeams = new JavaScriptSerializer().Deserialize<IEnumerable<PilotRaceTeamDTO>>(json2);

            //reset page number to 1 when you filter a row
            if (searchString != null)
            {
                pageNumber = 1;
            }

            //the actual filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                pilotRaceTeams = pilotRaceTeams.Where(s => s.Team.Name.ToLower().Contains(searchString.ToLower()));
            }

            pilotRaceTeams = pilotRaceTeams.OrderBy(r => r.Team.Name).ThenBy(r => r.Pilot.Name);

            int pageSize = 50;
            PilotRaceTeamModel pilotRaceTeamModel = new PilotRaceTeamModel(result, PaginatedList<PilotRaceTeamDTO>.CreateAsync(pilotRaceTeams.AsQueryable(), pageNumber ?? 1, pageSize));

            return View("Details", pilotRaceTeamModel);
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

            try
            {
                var result = _client.UploadString("races/update/" + Id, JsonConvert.SerializeObject(raceToEdit));
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the race, try a different name and make sure the dates of the race are in between the dates of the season or try again.");
                return View("Exception", Exception);
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
            try
            {
                _client.UploadString("races/delete/" + id, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the race, make sure there is no connection to a team or pilot.");
                return View("Exception", Exception);
            }
            
            return Redirect("Index");
        }

        //to show and fill teams dropdown
        public IActionResult AddTeam(int id)
        {
            string json3 = _client.DownloadString("races/" + id);
            var race = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json3);

            var json = _client.DownloadString("teams");
            var teams = new JavaScriptSerializer().Deserialize<IEnumerable<TeamsDTO>>(json);

            AddPilotRaceTeamModel addPilotRaceTeamModel = new(teams, race);
            addPilotRaceTeamModel.RaceId = id;
            return View(addPilotRaceTeamModel);
        }

        //to show and fill pilotdropdown
        public IActionResult AddPilot(AddPilotRaceTeamModel modelView)
        {           
            var json = _client.DownloadString("pilots");
            var pilots = new JavaScriptSerializer().Deserialize<IEnumerable<PilotsDTO>>(json).ToList();

            var getAllPilotsFromTeamAndRace = _client.DownloadString("pilotraceteam/GetPilotsByRaceIdTeamId/" + modelView.RaceId + "/" + modelView.TeamId);
            var pilotsToExclude = new JavaScriptSerializer().Deserialize<IEnumerable<PilotRaceTeamDTO>>(getAllPilotsFromTeamAndRace);

            //To make sure I can't add the same Pilot to the Same team twice, I filter the list of pilots
            //I ToList() the IEnumerable pilots to call the .Remove() method
            foreach (var p in pilots.ToList())
            {
                foreach (var pte in pilotsToExclude)
                {
                    if (p.Id == pte.PilotId)
                    {
                        pilots.Remove(p);
                    }
                }
            }

            //have to get race by id again otherwise modelview.race is null
            string json2 = _client.DownloadString("races/" + modelView.RaceId);
            var race = (new JavaScriptSerializer()).Deserialize<RacesDTO>(json2);

            //have to get Team by id to fill in the name
            string json3 = _client.DownloadString("teams/" + modelView.TeamId);
            var team = (new JavaScriptSerializer()).Deserialize<TeamsDTO>(json3);

            AddPilotRaceTeamModel addPilotRaceTeamModel = new(pilots, race, team);
            addPilotRaceTeamModel.RaceId = race.Id;
            addPilotRaceTeamModel.TeamId = modelView.TeamId;
            return View(addPilotRaceTeamModel);
        }

        public IActionResult AddTeamAndPilotToRaces(AddPilotRaceTeamModel modelView)
        {
            PilotRaceTeamDTO pilotRaceTeam = new();

            pilotRaceTeam.RaceId = modelView.RaceId;
            pilotRaceTeam.TeamId = int.Parse(modelView.TeamId);
            pilotRaceTeam.PilotId = int.Parse(modelView.PilotId);
            try
            {
                string data = JsonConvert.SerializeObject(pilotRaceTeam);
                var result = _client.UploadString("pilotraceteam/add", data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when connecting a team and a pilot.");
                return View("Exception", Exception);
            }
            return Redirect("Details/" + pilotRaceTeam.RaceId);
        }

        public IActionResult DeletePilotRaceTeam(int pilotId, int raceId, int teamId)
        {
            string json = _client.DownloadString("pilotraceteam/GetByIds/" + pilotId + "/" + raceId + "/" + teamId);
            var result = (new JavaScriptSerializer()).Deserialize<PilotRaceTeamDTO>(json);
            return View("DeletePilotRaceTeam", result);
        }

        public IActionResult DeletePilotRaceTeams(int pilotId, int raceId, int teamId)
        {
            try
            {
                _client.UploadString("pilotraceteam/delete/" + pilotId + "/" + raceId + "/" + teamId, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting a connection to team and pilot.");
                return View("Exception", Exception);
            }      
            return Redirect("Details/" + raceId);
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
