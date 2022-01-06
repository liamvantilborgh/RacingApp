using Microsoft.AspNetCore.Mvc.Rendering;
using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class PilotModel
    {
        public int Id { get; set; }
        public string LengthFeet { get; set; }
        public string Wheightlbs { get; set; }
        public string Sex { get; set; }
        public bool feet { get; set; }
        public bool lbs { get; set; }
        public PilotsDTO Pilot { get; set; }
        public PilotModel()
        {
            Pilot = new();
        }
        public PilotModel(PilotsDTO pilot)
        {
            Pilot = pilot;
            //otherwise Id in view is 0
            Id = pilot.Id;
            Sex = pilot.Sex.ToString();
            LengthFeet = Math.Round(pilot.Length / 30.48, 1).ToString();
            Wheightlbs = Math.Round(pilot.Weight * 2.205, 1).ToString();
        }
    }

    public enum Sex
    {
        V,
        M
    }
}
