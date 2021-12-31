using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Seasons
    {
        public int Id { get; set; }
        public Series Series { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public ICollection<Races> Races { get; set; }
        public Seasons()
        {
            Races = new Collection<Races>();
        }
    }
}
