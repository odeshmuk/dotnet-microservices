using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope()){
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context){
            if(!context.Platforms.Any()){
                Console.Write("Seeding Data...");

                context.Platforms.AddRange(
                    new Platform(){ID=1,Name="Dot Net",Publisher="Microsoft",Cost="Free"},
                    new Platform(){ID=2,Name="SQL Server",Publisher="Microsoft",Cost="Free"},
                     new Platform(){ID=3,Name="Kubernetes",Publisher="Test",Cost="Free"}
                );

                context.SaveChanges();
            }
            else{
                Console.Write("We already have data");
            }
        }
    }
}