using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Pilots
    {
        public Pilots()
        {
            PilotRaceTeam = new Collection<PilotRaceTeam>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string LicenseNumber { get; set; }
        public string PhotoRelativePath { get; set; }
        public char Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Length { get; set; }
        public decimal Weight { get; set; }
        public ICollection<PilotRaceTeam> PilotRaceTeam { get; set; }
    }
}
