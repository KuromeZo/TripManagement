using Microsoft.EntityFrameworkCore;
using TripManagement.Models;

namespace TripManagement.Data;

public class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }
        public DbSet<CountryTrip> CountryTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);
                entity.Property(e => e.IdClient).UseIdentityColumn();
                entity.ToTable("Client");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.IdTrip);
                entity.Property(e => e.IdTrip).UseIdentityColumn();
                entity.ToTable("Trip");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry);
                entity.Property(e => e.IdCountry).UseIdentityColumn();
                entity.ToTable("Country");
            });

            // (many-to-many)
            modelBuilder.Entity<ClientTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdClient, e.IdTrip });
                entity.ToTable("Client_Trip");

                entity.HasOne(e => e.Client)
                    .WithMany(e => e.ClientTrips)
                    .HasForeignKey(e => e.IdClient)
                    .HasConstraintName("Table_5_Client");

                entity.HasOne(e => e.Trip)
                    .WithMany(e => e.ClientTrips)
                    .HasForeignKey(e => e.IdTrip)
                    .HasConstraintName("Table_5_Trip");
            });

            // (many-to-many)
            modelBuilder.Entity<CountryTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdCountry, e.IdTrip });
                entity.ToTable("Country_Trip");

                entity.HasOne(e => e.Country)
                    .WithMany(e => e.CountryTrips)
                    .HasForeignKey(e => e.IdCountry)
                    .HasConstraintName("Country_Trip_Country");

                entity.HasOne(e => e.Trip)
                    .WithMany(e => e.CountryTrips)
                    .HasForeignKey(e => e.IdTrip)
                    .HasConstraintName("Country_Trip_Trip");
            });
        }
    }