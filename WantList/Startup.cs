using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WantList.Anidb;
using WantList.Data;
using WantList.Data.Interfaces;
using WantList.Data.Sql;
using WantList.MangaUpdates;

namespace WantList
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<WantListDbContext>(options =>
            {
                if (_environment.IsDevelopment() && Configuration.GetValue<bool>("LogQueries"))
                {
                    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                }

                options.UseMySQL(Configuration.GetConnectionString("WantlistDb"));
            });

            services.AddScoped<IAnidbAnimeData, SqlAnidbAnimeData>();
            services.AddScoped<IAnimeData, SqlAnimeData>();
            services.AddScoped<IMangaData, SqlMangaData>();
            services.AddScoped<ISettingsData, SqlSettingsData>();
            services.AddScoped<AnidbSync>();
            services.AddScoped<AnidbService>();
            services.AddScoped<IMangaUpdatesService, MangaUpdatesService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AnidbSync anidbSync, IMangaUpdatesService mangaUpdatesService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            mangaUpdatesService.GetData(15);
            anidbSync.OnStartup();
        }
    }
}