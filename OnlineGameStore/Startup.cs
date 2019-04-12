using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Data.Services;
using OnlineGameStore.Domain.Entities;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddTransient<IValidatorStrategy<GameModel>, DefaultValidatorStrategy<GameModel>>();
            services.AddAutoMapper();

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OnlineGameContext onlineGameContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            MapperHelper.InitMapperConf();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
