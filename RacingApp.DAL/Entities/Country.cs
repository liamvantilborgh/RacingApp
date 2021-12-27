using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.DAL.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Circuits> Circuits { get; set; }

        public Country()
        {
            Circuits = new Collection<Circuits>();
        }
    }
}
