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
       ISeriesRepository Series { get; }
       ISeasonsRepository Seasons { get; }
       IRacesRepository Races { get; }
       ITeamsRepository Teams { get; }
       IPilotsRepository Pilots { get; }
       IPilotRaceTeamRepository PilotRaceTeam { get; }
       int CommitAsync();
    }
}
