using Microsoft.AspNetCore.Mvc.Rendering;
using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class AddPilotRaceTeamModel
    {
        public List<SelectListItem> TeamsDropDown;
        public List<SelectListItem> PilotDropDown;
        public RacesDTO Race { get; set; }
        public TeamsDTO Team { get; set; }
        public int RaceId { get; set; }
        public string TeamId { get; set; }
        public string PilotId { get; set; }

        public AddPilotRaceTeamModel(IEnumerable<TeamsDTO> teams, RacesDTO race)
        {
            Race = race;
            RaceId = race.Id;
            TeamsDropDown = new List<SelectListItem>();

            foreach (var item in teams)
            {
                TeamsDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }          
        }

        public AddPilotRaceTeamModel(IEnumerable<PilotsDTO> pilots, RacesDTO race, TeamsDTO team)
        {
            Race = race;
            Team = team;
            //RaceId = race.Id;
            PilotDropDown = new List<SelectListItem>();

            foreach (var item in pilots)
            {
                PilotDropDown.Add(new SelectListItem
                {
                    Text = item.Name + " " + item.FirstName,
                    Value = item.Id.ToString()
                });
            }
        }

        public AddPilotRaceTeamModel() { }
    }
}
