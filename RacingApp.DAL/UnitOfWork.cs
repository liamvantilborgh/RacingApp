using RacingApp.DAL.Data;
using RacingApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RacingAppContext context;
        private CountryRepository _countryRepository;
        private CircuitsRepository _circuitsRepository;
        private SeriesRepository _seriesRepository;
        private SeasonsRepository _seasonsRepository;
        private RacesRepository _racesRepository;
        private TeamsRepository _teamsRepository;
        private PilotsRepository _pilotsRepository;
        private PilotRaceTeamRepository _pilotRaceTeamRepository;
        public UnitOfWork(RacingAppContext context)
        {
            this.context = context;
        }
        public ICountryRepository Countries => _countryRepository ??= new CountryRepository(context);
        public ICircuitsRepository Circuits => _circuitsRepository ??= new CircuitsRepository(context);
        public ISeriesRepository Series => _seriesRepository ??= new SeriesRepository(context);
        public ISeasonsRepository Seasons => _seasonsRepository ??= new SeasonsRepository(context);
        public IRacesRepository Races => _racesRepository ??= new RacesRepository(context);
        public ITeamsRepository Teams => _teamsRepository ??= new TeamsRepository(context);
        public IPilotsRepository Pilots => _pilotsRepository ??= new PilotsRepository(context);
        public IPilotRaceTeamRepository PilotRaceTeam => _pilotRaceTeamRepository ??= new PilotRaceTeamRepository(context);

        public int CommitAsync()
        {
            //int test = 0;
            return context.SaveChanges();
            /*try
            {
                test = 
            }
            catch(Exception E)
            {
                Debug.WriteLine("UOW");
                Debug.WriteLine(E.InnerException.Message);
                throw new Exception(E.InnerException.Message);
            }

            return test;*/
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
