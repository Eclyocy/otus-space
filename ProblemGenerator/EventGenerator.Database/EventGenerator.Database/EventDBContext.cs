using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventGenerator.Database
{
    public class EventDBContext : DbContext
    {
        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        //public DbSet<Ship> Ships { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            Console.WriteLine(config.GetConnectionString("DefaultConnection"));

            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name)
                ?? throw new Exception(string.Format("{0} environment variable must be specified", name));
        }
    }
}
