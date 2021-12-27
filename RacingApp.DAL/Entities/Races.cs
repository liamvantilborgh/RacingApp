using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Races
    {
        public int Id { get; set; }
        [ForeignKey("SeasonId")]
        public Seasons Season { get; set; }
        public int SeasonId { get; set; }
        [ForeignKey("CircuitId")]
        public Circuits Circuit { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
