using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Circuits
    {
        public int Id { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int Length_Circuit { get; set; }
        public string Street_Name { get; set; }
        public int House_Number { get; set; }
        public string City { get; set; }
        public int Postal_Code { get; set; }
    }
}
