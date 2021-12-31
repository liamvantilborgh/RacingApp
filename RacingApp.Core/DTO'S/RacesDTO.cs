using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class RacesDTO
    {
        public int Id { get; set; }
        public SeasonsDTO Season { get; set; }
        public int SeasonId { get; set; }
        public CircuitsDTO Circuit { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
