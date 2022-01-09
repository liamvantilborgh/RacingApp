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
    public class PilotsController : Controller
    {
        private WebClient _client;
        public PilotsController()
        {
            GetWebClient();
        }

        public IActionResult Index()
        {
            string json = _client.DownloadString("pilots");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<PilotsDTO>>(json);
            result = result.OrderBy(r => r.Name);
            return View("Index", result);
        }

        public IActionResult Create()
        {
            PilotModel pilotModel = new PilotModel();
            //otherwise date is 0001 which is hard to work with so I set it to today
            pilotModel.Pilot.DateOfBirth = DateTime.Today;
            return View(pilotModel);
        }
        public IActionResult Add(PilotModel pilotModel)
        {
            PilotsDTO pilot = pilotModel.Pilot;
            pilot.Sex = char.Parse(pilotModel.Sex);
            if (pilotModel.feet)
            {
                var cm = Math.Round(double.Parse(pilotModel.LengthFeet) * 30.48);
                pilot.Length = (int)cm;
            }
            if (pilotModel.lbs)
            {
                var kg = Math.Round((double.Parse(pilotModel.Wheightlbs) / 2.205), 1);
                pilot.Weight = kg;
            }

            try
            {
                string data = JsonConvert.SerializeObject(pilot);
                var result = _client.UploadString("pilots/add", data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the pilot, try a different license number or try again.");
                return View("Exception", Exception);
            }
            
            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("pilots/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<PilotsDTO>(json);
            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("pilots/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<PilotsDTO>(json);

            PilotModel pilotModel = new PilotModel(result);
            return View("Edit", pilotModel);
        }

        public IActionResult EditPilot(int Id, PilotModel pilotModel)
        {
            PilotsDTO pilot = pilotModel.Pilot;
            pilot.Sex = char.Parse(pilotModel.Sex);
            if (pilotModel.feet)
            {
                var cm = Math.Round(double.Parse(pilotModel.LengthFeet) * 30.48);
                pilot.Length = (int)cm;
            }
            if (pilotModel.lbs)
            {
                var kg = Math.Round((double.Parse(pilotModel.Wheightlbs) / 2.205), 1);
                pilot.Weight = kg;
            }

            try
            {
                string data = JsonConvert.SerializeObject(pilot);
                var result = _client.UploadString("pilots/update/" + Id, data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the pilot, try a different license number or try again.");
                return View("Exception", Exception);
            }

            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("pilots/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<PilotsDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeletePilot(int id)
        {
            try
            {
                _client.UploadString("pilots/delete/" + id, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the pilot, make sure there is no connection to a race or team.");
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
