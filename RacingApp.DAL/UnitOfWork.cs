﻿using RacingApp.DAL.Data;
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
        private CircuitsRepository _circuitsRepository;
        private SeriesRepository _seriesRepository;
        public UnitOfWork(RacingAppContext context)
        {
            this.context = context;
        }
        public ICountryRepository Countries => _countryRepository ??= new CountryRepository(context);
        public ICircuitsRepository Circuits => _circuitsRepository ??= new CircuitsRepository(context);
        public ISeriesRepository Series => _seriesRepository ??= new SeriesRepository(context);

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
