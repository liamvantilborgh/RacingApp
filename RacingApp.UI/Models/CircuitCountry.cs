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
        public int Id { get; set; }
        public int LengthMiles { get; set; }
        public bool miles { get; set; }
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
        public CircuitCountry(List<CountryDTO> countries, CircuitsDTO circuit)
        {
            Circuit = circuit;
            Id = circuit.Id;
            LengthMiles = (int)Math.Round(circuit.Length_Circuit * 0.62137);
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
