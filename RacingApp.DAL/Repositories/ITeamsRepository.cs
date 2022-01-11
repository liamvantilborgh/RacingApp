using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public interface ITeamsRepository : IRepository<Teams>
    {
        public void DeleteWithInlineQuery(int id);
    }
}
