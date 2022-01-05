using Microsoft.AspNetCore.Mvc.Rendering;
using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class SeasonsSerie
    {
        public string SeriesId { get; set; }
        public int Id { get; set; }
        public SeasonsDTO Season { get; set; }
        public List<SelectListItem> SeriesDropDown;

        public SeasonsSerie(List<SeriesDTO> series)
        {
            Season = new SeasonsDTO();
            //have to set the dates otherwise they default to the year 0001 in view
            Season.Startdate = DateTime.Today;
            Season.Enddate = DateTime.Today.AddDays(1);
            SeriesDropDown = new List<SelectListItem>();

            foreach (var item in series)
            {
                SeriesDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
        }

        public SeasonsSerie(List<SeriesDTO> series, SeasonsDTO season)
        {
            Season = season;
            Id = season.Id;
            //to set right series in dropdown
            SeriesId = season.SeriesId.ToString();
            SeriesDropDown = new List<SelectListItem>();

            foreach (var item in series)
            {
                SeriesDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
        }

        public SeasonsSerie()
        {

        }
    }
}
