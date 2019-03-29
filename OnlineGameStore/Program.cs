﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

//            using (var scope = host.Services.CreateScope())
//            {
//                try
//                {
//                    var context = scope.ServiceProvider.GetService<OnlineGameContext>();
//                    context.Database.Migrate();
//                    context.EnsureSeedDataForContext();
//                }
//                catch (System.Exception ex)
//                {
//                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
//                }
//            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
