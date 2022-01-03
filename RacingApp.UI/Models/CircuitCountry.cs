using Microsoft.AspNetCore.Mvc.Rendering;
using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class CircuitCountry
    {
        public string CountryId { get; set; }
        public CircuitsDTO Circuit { get; set; }
        public List<SelectListItem> CountryDropDown;

        public CircuitCountry(List<CountryDTO> countries)
        {
            Circuit = new CircuitsDTO();
            CountryDropDown = new List<SelectListItem>();

            foreach (var item in countries)
            {
                CountryDropDown.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
        }
        public CircuitCountry()
        {
        }
    }
}
