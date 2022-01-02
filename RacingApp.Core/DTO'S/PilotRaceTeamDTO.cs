using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class PilotRaceTeamDTO
    {
        public int PilotId { get; set; }
        public int RaceId { get; set; }
        public int TeamId { get; set; }
        public PilotsDTO Pilot { get; set; }
        public RacesDTO Race { get; set; }
        public TeamsDTO Team { get; set; }
    }
}
