using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdysseyServer.Persistence;
using Microsoft.EntityFrameworkCore;
using OdysseyServer.Services;
using OdysseyServer.Services.Contracts;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Repository;
using OdysseyServer.Api.Middleware;
using AutoMapper;
using OdysseyServer.Services.Mappers;
using System.Reflection;
using System.IO;
using System;
using StackExchange.Redis;
using OdysseyServer.Api.Binders;

namespace OdysseyServer.Api
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
            services.AddControllers(configure => {
                configure.ModelBinderProviders.Insert(0, new ProtobufMessageBinderProvider());
            });
            services.AddSwaggerGen();
            services.AddDbContext<OdysseyDbContext>(options => {
                options.UseSqlServer(Configuration["DbConfiguration:ConnectionStrings"]);
                options.EnableSensitiveDataLogging();
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            string redisConfiguration = Configuration["CacheConfiguration:Host"];
            string sqlcacheConfiguration = Configuration["DbConfiguration:CacheConnectionString"];

            if (!string.IsNullOrEmpty(redisConfiguration))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConfiguration;
                });
            }
            else if (!string.IsNullOrEmpty(sqlcacheConfiguration))
            {
                services.AddDistributedSqlServerCache(options =>
                {
                    options.ConnectionString = sqlcacheConfiguration;
                    options.SchemaName = "dbo";
                    options.TableName = "odysseydb";
                });
            }
            else
            {
                throw new NotSupportedException("Neither SQL nor Redis cache is configured.");
            }

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IAbilityService, AbilityService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Odyssey API V1");
            });

            app.UseCors(options => options
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
