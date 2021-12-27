using RacingApp.DAL.Data;
using RacingApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RacingAppContext context;
        private CountryRepository _countryRepository;
        public UnitOfWork(RacingAppContext context)
        {
            this.context = context;
        }
        public ICountryRepository Countries => _countryRepository ??= new CountryRepository(context);

        public int CommitAsync()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
