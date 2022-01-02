using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Races
    {
        public Races()
        {
            PilotRaceTeam = new Collection<PilotRaceTeam>();
        }
        public int Id { get; set; }
        public Seasons Season { get; set; }
        public int SeasonId { get; set; }
        public Circuits Circuit { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public ICollection<PilotRaceTeam> PilotRaceTeam { get; set; }
    }
}
