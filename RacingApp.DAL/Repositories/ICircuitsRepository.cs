using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public interface ICircuitsRepository : IRepository<Circuits>
    {
        IEnumerable<Circuits> GetAllWithCountry();
        IEnumerable<Circuits> GetAllByCountryId(int countryId);
        IEnumerable<Circuits> GetByIdWithCountry(int id);
    }
}
