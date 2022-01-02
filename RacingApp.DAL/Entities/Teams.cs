using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Teams
    {
        public Teams()
        {
            PilotRaceTeam = new Collection<PilotRaceTeam>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PilotRaceTeam> PilotRaceTeam { get; set; }
    }
}
