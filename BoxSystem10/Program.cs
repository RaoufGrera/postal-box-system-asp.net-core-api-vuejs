using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SystemData;
using SystemData.Models;

namespace BoxSystem10
{
    public class Program
    {
        public static void Main(string[] args)
        {

           /*     var host = new WebHostBuilder()
     .UseContentRoot(Directory.GetCurrentDirectory())
     .UseKestrel()
     .UseIISIntegration()
     .UseStartup<Startup>()
    
     .Build();

            host.Run();*/
        IWebHost host = BuildWebHost(args);

            /*     using (IServiceScope scope = host.Services.CreateScope())
              {
                  IServiceProvider services = scope.ServiceProvider;
                  SystemContext context = services.GetRequiredService<SystemContext>();

                  var manager = scope.ServiceProvider.GetService<UserManager<AppUser>>();

                  try
                  {
                    SeedData.Initialize(serviceProvider: services);
                       SeedData.SeedUsers(manager, context).Wait();
                  }
                  catch (Exception ex)
                  {
                      ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                      logger.LogError(ex, "An error occurred seeding the DB.");
                  }
              }; */
            host.Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
      
                     
            .Build();
    }
}
