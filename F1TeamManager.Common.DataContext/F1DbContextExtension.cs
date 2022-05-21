using F1.Common.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1TeamManager.Common.DataContext
{
    public static class F1DbContextExtension
    {
        public static IServiceCollection AddF1TeamContext(
        this IServiceCollection services, string relativePath = "..")
        {
            string databasePath = Path.Combine(relativePath, "f1tm.db");

            services.AddDbContext<F1DbContext>(options =>
              options.UseSqlite($"Data Source={databasePath}")
            );

            return services;
        }
    }
}
