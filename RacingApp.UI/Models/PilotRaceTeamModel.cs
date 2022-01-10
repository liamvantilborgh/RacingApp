using RacingApp.Core.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models
{
    public class PilotRaceTeamModel
    {
        public RacesDTO Race;
        public PaginatedList<PilotRaceTeamDTO> PilotRaceTeams;
        public PilotRaceTeamModel(RacesDTO race, PaginatedList<PilotRaceTeamDTO> pilotRaceTeams)
        {
            Race = race;
            PilotRaceTeams = pilotRaceTeams;
        }
    }
}
