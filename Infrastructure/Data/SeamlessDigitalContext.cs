using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TestProjectContextFactory : IDesignTimeDbContextFactory<SeamlessDigitalContext>
    {
        public SeamlessDigitalContext CreateDbContext(string[] args)
        {
            var conbuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            IConfigurationRoot config = conbuilder.Build();

            var builder = new DbContextOptionsBuilder<SeamlessDigitalContext>();
            builder.UseSqlServer(config["ConnectionStrings:SeamlessDigitalContext"]);
            return new SeamlessDigitalContext(builder.Options);
        }

    }

    public class SeamlessDigitalContext : DbContext
    {
        public SeamlessDigitalContext(DbContextOptions<SeamlessDigitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }

    }
}
