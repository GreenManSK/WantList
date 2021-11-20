using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
        public const string StaticImagesPath = "/static/images";
        public const string BaseUrl = "/wl/";
        private const string ClientUrl = "";
        
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
            if (_environment.IsDevelopment())
            {
                services.AddCors(o => o.AddPolicy("DevelopmentPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
            }
            
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AnidbSync anidbSync)
        {
            app.UsePathBase(BaseUrl);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DevelopmentPolicy");
            }
            
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/";
                    await next();
                }
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetValue<string>("ImagesPath")),
                RequestPath = StaticImagesPath
            });

            app.UseDefaultFiles(GetDefaultFileOptions());
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetValue<string>("ClientBuildPath")),
                RequestPath = ClientUrl
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api", async context =>
                {
                    context.Response.ContentType = "text/*; charset=utf-8";
                    await context.Response.WriteAsync("がんばルビィ");
                });
                endpoints.MapControllers();
            });

            anidbSync.OnStartup();
        }

        private DefaultFilesOptions GetDefaultFileOptions()
        {
            var path = Configuration.GetValue<string>("ClientBuildPath");
            var fileProvider = new PhysicalFileProvider(path);

            var options = new DefaultFilesOptions
            {
                FileProvider = fileProvider,
                RequestPath = ClientUrl
            };
            options.DefaultFileNames.Add("index.html");
            return options;
        }
    }
}