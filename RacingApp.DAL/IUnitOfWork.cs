using RacingApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL
{
    public interface IUnitOfWork : IDisposable
    {
       ICountryRepository Countries { get; }
       ICircuitsRepository Circuits { get; }
       int CommitAsync();
    }
}
