using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class CircuitsDTO
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int Length_Circuit { get; set; }
        public string Street_Name { get; set; }
        public int House_Number { get; set; }
        public string City { get; set; }
        public int Postal_Code { get; set; }
        public CountryDTO Country { get; set; }
    }
}
