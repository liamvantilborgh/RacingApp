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

        public IActionResult Index(string sortOrder, string currentFilter, string searchStringSerie, DateTime? searchStringStartDate, string searchStringActive, int? pageNumber, int currentSize, int? customSize)
        {
            //so it knows what i'm trying to filter or sort
            ViewData["CurrentSort"] = sortOrder;
            ViewData["StartDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "startdate_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["EndDateSortParm"] = sortOrder == "enddate" ? "enddate_desc" : "enddate";
            ViewData["SeriesSortParm"] = sortOrder == "series" ? "series_desc" : "series";
            ViewData["ActiveSortParm"] = sortOrder == "active" ? "active_desc" : "active";
            ViewData["CurrentFilterSerie"] = searchStringSerie;
            ViewData["CurrentFilterStartDate"] = searchStringStartDate;
            ViewData["CurrentFilterActive"] = searchStringActive;
            ViewData["CurrentPageSize"] = customSize;

            //to make sure the standard pagesize is 50
            int pageSize = 50;
            if (currentSize == 0)
            {
                currentSize = pageSize;
            }

            //get all the seasons
            string json = _client.DownloadString("seasons");
            var result = (new JavaScriptSerializer()).Deserialize<IEnumerable<SeasonsDTO>>(json);

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
            if (searchStringSerie != null || searchStringStartDate != null || searchStringActive != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchStringSerie = currentFilter;
            }
            if (searchStringStartDate != null)
            {
                pageNumber = 1;
            }
            else
            {
                try
                {
                    searchStringStartDate = DateTime.Parse(currentFilter);
                }
                catch { }
            }
            if (searchStringActive != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchStringActive = currentFilter;
            }
            //remembers custom page size when switch pages
            if (customSize != null)
            {
                pageSize = 1;
            }
            else
            {
                customSize = currentSize;
            }

            //the actual filtering
            if (!String.IsNullOrEmpty(searchStringSerie))
            {
                result = result.Where(s => s.Series.Name.ToLower().Contains(searchStringSerie.ToLower()));
            }
            if(searchStringStartDate.HasValue)
            {
                result = result.Where(s => s.Startdate == searchStringStartDate);
            }
            if (!String.IsNullOrEmpty(searchStringActive))
            {
                //can later make it a dropdown in view
                if (searchStringActive.ToLower() == "active")
                {
                    result = result.Where(s => s.Active == true);
                }
                else if (searchStringActive.ToLower() == "inactive")
                {
                    result = result.Where(s => s.Active == false);
                }
            }
            if (customSize.HasValue)
            {
                pageSize = (int)customSize;
            }

            //sorting
            switch (sortOrder)
            {
                case "startdate_desc":
                    result = result.OrderByDescending(s => s.Startdate);
                    break;
                case "name":
                    result = result.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(s => s.Name);
                    break;
                case "enddate":
                    result = result.OrderBy(s => s.Enddate);
                    break;
                case "enddate_desc":
                    result = result.OrderByDescending(s => s.Enddate);
                    break;
                case "series":
                    result = result.OrderBy(s => s.Series.Name);
                    break;
                case "series_desc":
                    result = result.OrderByDescending(s => s.Series.Name);
                    break;
                case "active":
                    result = result.OrderBy(s => s.Active);
                    break;
                case "active_desc":
                    result = result.OrderByDescending(s => s.Active);
                    break;
                default:
                    result = result.OrderBy(r => r.Startdate);
                    break;
            }

            //here you can edit the amount of results per page
            return View("Index", PaginatedList<SeasonsDTO>.CreateAsync(result.AsQueryable(), pageNumber ?? 1, pageSize));
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
            if (seasonSerie.Season.Startdate > DateTime.Today || seasonSerie.Season.Enddate < DateTime.Today)
            {
                seasonSerie.Season.Active = false;
            }
            else //if(seasonSerie.Season.Enddate.Equals(null))
            {
                seasonSerie.Season.Active = true;
            }

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
            if (seasonSerie.Season.Startdate > DateTime.Today || seasonSerie.Season.Enddate < DateTime.Today)
            {
                seasonSerie.Season.Active = false;
            }
            else //if(seasonSerie.Season.Enddate.Equals(null))
            {
                seasonSerie.Season.Active = true;
            }
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
