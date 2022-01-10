using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Sort_Order { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public ICollection<Seasons> Seasons { get; set; }
        public Series()
        {
            Seasons = new Collection<Seasons>();
        }
    }
}
