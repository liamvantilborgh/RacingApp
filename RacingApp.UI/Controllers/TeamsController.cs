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
    public class TeamsController : Controller
    {
        private WebClient _client;
        public TeamsController()
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

            string json = _client.DownloadString("teams");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<TeamsDTO>>(json);
            //remembers custom page size when switch pages
            if (customSize != null)
            {
                pageSize = 1;
            }
            else
            {
                customSize = currentSize;
            }

            if (customSize.HasValue)
            {
                pageSize = (int)customSize;
            }
            ViewData["Currentsize"] = customSize;

            result = result.OrderBy(r => r.Name);

            return View("Index", PaginatedList<TeamsDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(TeamsDTO team)
        {
            try
            {
                string data = JsonConvert.SerializeObject(team);
                var result = _client.UploadString("teams/add", data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when creating the team, try a different name or try again.");
                return View("Exception", Exception);
            }

            return Redirect("Index");
        }

        public IActionResult Details(int id)
        {
            string json = _client.DownloadString("teams/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<TeamsDTO>(json);

            return View("Details", result);
        }

        public IActionResult Edit(int id)
        {
            string json = _client.DownloadString("teams/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<TeamsDTO>(json);

            return View("Edit", result);

        }

        public IActionResult EditTeam(int Id, TeamsDTO team)
        {
            try
            {
                string data = JsonConvert.SerializeObject(team);
                var result = _client.UploadString("teams/update/" + Id, data);
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when updating the team, try a different name or try again.");
                return View("Exception", Exception);
            }

            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("teams/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<TeamsDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteTeam(int id)
        {
            try
            {
                _client.UploadString("teams/delete/" + id, "POST", "");
            }
            catch
            {
                ExceptionModel Exception = new ExceptionModel("Something went wrong when deleting the team, make sure there is no connection to a race or pilot.");
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
