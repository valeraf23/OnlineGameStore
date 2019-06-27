using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Data.Services.Implementations;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using OnlineGameStore.Api.Filters;
using OnlineGameStore.Data.ValidationRules;

namespace OnlineGameStore.Api
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

            services.AddDbContext<OnlineGameContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Local"),
                    b => b.MigrationsAssembly("OnlineGameStore.Api")));

            services.AddMvc(
                    opt => { opt.Filters.Add(typeof(ValidatorActionFilter)); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<GameModelValidator>(); });

            services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IRepository<Genre>, GenresRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IRepository<PlatformType>, PlatformTypeRepository>();
            services.AddScoped<IPlatformTypeService, PlatformTypeService>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Publisher>, PublisherRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddTransient<IValidatorStrategy<GameModel>, DefaultValidatorStrategy<GameModel>>();
            services.AddTransient<IValidatorStrategy<GenreModel>, DefaultValidatorStrategy<GenreModel>>();
            services.AddTransient<IValidatorStrategy<PlatformTypeModel>, DefaultValidatorStrategy<PlatformTypeModel>>();
            services.AddTransient<IValidatorStrategy<PublisherModel>, DefaultValidatorStrategy<PublisherModel>>();

            services.AddHttpClient();
            services.AddTransient<ITypeHelperService, TypeHelperService>();
            services.AddHttpCacheHeaders(
                expirationModelOptions => expirationModelOptions.MaxAge = 60,
                validationModelOptions => validationModelOptions.MustRevalidate = true);

            services.AddMemoryCache();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            OnlineGameContext onlineGameContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
            }

            MapperHelper.InitMapperConf();
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
