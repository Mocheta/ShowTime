using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ShowTime.DataAccess
{
    public class ShowTimeDBContext : DbContext
    {
        public ShowTimeDBContext(DbContextOptions<ShowTimeDBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = DENIS\\SQLEXPRESS; Initial Catalog = ShowTimeDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Lineup> Lineups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new Configurations.FestivalConfiguration().Configure(modelBuilder.Entity<Festival>());
            new Configurations.ArtistConfiguration().Configure(modelBuilder.Entity<Artist>());
            new Configurations.LineupConfiguration().Configure(modelBuilder.Entity<Lineup>());
            new Configurations.UserConfiguration().Configure(modelBuilder.Entity<User>());
            new Configurations.BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
        }
    }
}
