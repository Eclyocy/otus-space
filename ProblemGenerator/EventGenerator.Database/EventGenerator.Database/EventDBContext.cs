using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
