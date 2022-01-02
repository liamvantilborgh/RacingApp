using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Data;
using RacingApp.DAL;
using RacingApp.BLL;
using RacingApp.DAL.Repositories;

namespace RacingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Country
            services.AddTransient<CountryService>();
            //services.AddScoped<CountryRepository>();

            //Circuits
            services.AddTransient<CircuitsService>();
            //services.AddScoped<CircuitsRepository>();

            //Series
            services.AddTransient<SeriesService>();
            //services.AddScoped<SeriesRepository>();

            //Seasons
            services.AddTransient<SeasonsService>();
            //services.AddScoped<SeriesRepository>();

            //Races
            services.AddTransient<RacesService>();

            //Teams
            services.AddTransient<TeamsService>();

            //Pilots
            services.AddTransient<PilotsService>();

            //PilotRaceTeam
            services.AddTransient<PilotRaceTeamService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RacingApp.API", Version = "v1" });
            });

            services.AddDbContext<RacingAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RacingAppContext"),
                    x => x.MigrationsAssembly("RacingApp.DAL")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RacingApp.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
