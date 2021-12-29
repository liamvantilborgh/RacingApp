using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class SeriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Sort_Order { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public List<SeasonsDTO> Seasons { get; set; }
    }
}
