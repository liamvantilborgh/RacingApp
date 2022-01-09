using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class SeasonsDTO
    {
        public int Id { get; set; }
        public SeriesDTO Series { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public List<RacesDTO> Races { get; set; }
    }
}
