using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class PilotRaceTeam
    {
        public int PilotId { get; set; }
        public int RaceId { get; set; }  
        public int TeamId { get; set; }
        public Pilots Pilot { get; set; }
        public Races Race { get; set; }
        public Teams Team { get; set; }
    }
}
