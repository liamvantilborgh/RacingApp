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
    public class PilotsController : Controller
    {
        private WebClient _client;
        public PilotsController()
        {
            GetWebClient();
        }

        public IActionResult Index(string sortOrder, string searchStringName, string searchStringFirstName, string searchStringLicenseNumber, int? pageNumber)
        {
            //so it knows what i'm trying to filter or sort
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "firstname" ? "firstname_desc" : "firstname";
            ViewData["NickNameSortParm"] = sortOrder == "nickname" ? "nickname_desc" : "nickname";
            ViewData["LicenseNumberSortParm"] = sortOrder == "licensenumber" ? "licensenumber_desc" : "licensenumber";
            ViewData["CurrentFilterName"] = searchStringName;
            ViewData["CurrentFilterFirstName"] = searchStringFirstName;
            ViewData["CurrentFilterLicenseNumber"] = searchStringLicenseNumber;

            //get all the pilots
            string json = _client.DownloadString("pilots");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<PilotsDTO>>(json);

            //reset page number to 1 when you filter a row
            if (searchStringName != null || searchStringFirstName != null || searchStringLicenseNumber != null)
            {
                pageNumber = 1;
            }

            //the actual filtering
            if (!String.IsNullOrEmpty(searchStringName))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchStringName.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchStringFirstName))
            {
                result = result.Where(s => s.FirstName.ToLower().Contains(searchStringFirstName.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchStringLicenseNumber))
            {
                result = result.Where(s => s.LicenseNumber.ToLower().Contains(searchStringLicenseNumber.ToLower()));
            }

            //sorting
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(s => s.Name);
                    break;
                case "firstname":
                    result = result.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    result = result.OrderByDescending(s => s.FirstName);
                    break;
                case "nickname":
                    result = result.OrderBy(s => s.NickName);
                    break;
                case "nickname_desc":
                    result = result.OrderByDescending(s => s.NickName);
                    break;
                case "licensenumber":
                    result = result.OrderBy(s => s.LicenseNumber);
                    break;
                case "licensenumber_desc":
                    result = result.OrderByDescending(s => s.LicenseNumber);
                    break;
                default:
                    result = result.OrderBy(s => s.Name);
                    break;
            }

            //here you can edit the amount of results per page
            int pageSize = 50;
            return View("Index", PaginatedList<PilotsDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
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
            if (pilot.Length != null)
            {
                if(pilot.Length > 200)
                {
                    pilot.Length = 200;
                }
            }

            if (pilotModel.feet)
            {
                var cm = Math.Round(double.Parse(pilotModel.LengthFeet) * 30.48);
                if(cm > 200)
                {
                    pilot.Length = 200;
                }
                else
                {
                    pilot.Length = (int)cm;
                }
            }

            if (pilotModel.lbs)
            {
                var kg = Math.Round((double.Parse(pilotModel.Wheightlbs) / 2.205), 1);
                pilot.Weight = kg;
            }
            //in de service van pilot kreeg ik dit niet gedaan
            if(pilot.PhotoRelativePath == null)
            {
                pilot.PhotoRelativePath = "RacingApp/RacingApp.UI/Images/PilotAlt.png";
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
            //so that it doesn't give errors in the view when it contains a comma
            if(pilot.Weight != null)
            {
                pilot.Weight = Math.Round(double.Parse(pilotModel.Weight), 1);
            }
            if (pilot.Length != null)
            {
                if (pilot.Length > 200)
                {
                    pilot.Length = 200;
                }
            }

            if (pilotModel.feet)
            {
                //check if value is null before parsing
                if(pilotModel.LengthFeet != null)
                {
                    var cm = Math.Round(double.Parse(pilotModel.LengthFeet) * 30.48);
                    if (cm > 200)
                    {
                        pilot.Length = 200;
                    }
                    else
                    {
                        pilot.Length = (int)cm;
                    }
                }
            }

            if (pilotModel.lbs)
            {
                //check if value is null before parsing
                if (pilotModel.Wheightlbs != null)
                {
                    var kg = Math.Round((double.Parse(pilotModel.Wheightlbs) / 2.205), 1);
                    pilot.Weight = kg;
                }
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
