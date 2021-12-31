using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public interface IRacesRepository : IRepository<Races>
    {
        IEnumerable<Races> GetAllWithSeasonsCircuits();
        IEnumerable<Races> GetAllBySeasonsId(int seasonId);
        IEnumerable<Races> GetAllByCircuitsId(int circuitId);
        IEnumerable<Races> GetAllBySeasonsIdCircuitsId(int seasonId, int circuitsId);
        IEnumerable<Races> GetByIdWithSeasonsCircuits(int id);
    }
}
