using Microsoft.AspNetCore.Mvc;
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
    public class TeamsController : Controller
    {
        private WebClient _client;
        public TeamsController()
        {
            GetWebClient();
        }

        public IActionResult Index()
        {
            string json = _client.DownloadString("teams");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<TeamsDTO>>(json);
            return View("Index", result);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(TeamsDTO team)
        {
            string data = JsonConvert.SerializeObject(team);
            var result = _client.UploadString("teams/add", data);

            if (result.Length > 0)
            {
                return Redirect("Index");
            }

            return null;
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
            string data = JsonConvert.SerializeObject(team);
            var result = _client.UploadString("teams/update/" + Id, data);

            if (result.Length > 0)
            {
                return Redirect("Index");
            }

            return null;

        }

        public IActionResult Delete(int id)
        {
            string json = _client.DownloadString("teams/" + id);
            var result = (new JavaScriptSerializer()).Deserialize<TeamsDTO>(json);

            return View("Delete", result);

        }
        public IActionResult DeleteTeam(int id)
        {
            _client.UploadString("teams/delete/" + id, "POST", "");
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
