using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public interface ISeasonsRepository : IRepository<Seasons>
    {
        IEnumerable<Seasons> GetAllWithSeries();
        IEnumerable<Seasons> GetAllBySeriesId(int seriesId);
        IEnumerable<Seasons> GetByIdWithSeries(int id);
    }
}
