using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Data;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public class TeamsRepository : Repository<Teams>, ITeamsRepository
    {
        private readonly RacingAppContext _context;
        public TeamsRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteWithInlineQuery(int id)
        {
            var query = $"DELETE FROM [dbo].[Teams] WHERE id=(@id)";
            var param = new SqlParameter("@id", id);

            _context.Database.ExecuteSqlRaw(query, param);
        }
    }
}
