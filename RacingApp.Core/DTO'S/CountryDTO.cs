using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CircuitsDTO> Circuits { get; set; }
    }
}
