using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RayanBourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Domain.Context
{
    public class RayanBourseContext:DbContext
    {
        public RayanBourseContext(DbContextOptions<RayanBourseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("RayanBourseConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Product> Products { get; set; }

    }
}
