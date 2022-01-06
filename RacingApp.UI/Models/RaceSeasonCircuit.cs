using Microsoft.AspNetCore.Mvc.Rendering;
using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class RaceSeasonCircuit
    {
        public string SeasonId { get; set; }
        public string CircuitId { get; set; }
        public int Id { get; set; }
        public RacesDTO Race { get; set; }
        public List<SelectListItem> SeasonDropDown;
        public List<SelectListItem> CircuitDropDown;

        public RaceSeasonCircuit(List<SeasonsDTO> seasons, List<CircuitsDTO> circuits)
        {
            Race = new RacesDTO();
            //have to set the dates otherwise they default to the year 0001 in view
            Race.Startdate = DateTime.Today;
            Race.Enddate = DateTime.Today.AddDays(1);
            SeasonDropDown = new List<SelectListItem>();
            CircuitDropDown = new List<SelectListItem>();

            foreach (var item in seasons)
            {
                SeasonDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            foreach (var item in circuits)
            {
                CircuitDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
        }

        public RaceSeasonCircuit(List<SeasonsDTO> seasons, List<CircuitsDTO> circuits, RacesDTO race)
        {
            Race = race;
            Id = race.Id;
            //to set right series in dropdown
            SeasonId = race.SeasonId.ToString();
            CircuitId = race.CircuitId.ToString();
            SeasonDropDown = new List<SelectListItem>();
            CircuitDropDown = new List<SelectListItem>();

            foreach (var item in seasons)
            {
                SeasonDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            foreach (var item in circuits)
            {
                CircuitDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
        }

        public RaceSeasonCircuit()
        {

        }
    }
}
