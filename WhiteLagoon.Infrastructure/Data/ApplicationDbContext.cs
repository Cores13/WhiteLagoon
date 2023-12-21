using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 5,
                    Price = 200,
                    Sqm = 550
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqm = 550
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqm = 750
                }
            );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 1,

                },
                new VillaNumber
                {
                    Villa_Number = 102,
                    VillaId = 2,

                },
                new VillaNumber
                {
                    Villa_Number = 103,
                    VillaId = 3,
                },
                new VillaNumber
                {
                    Villa_Number = 201,
                    VillaId = 1,

                },
                new VillaNumber
                {
                    Villa_Number = 202,
                    VillaId = 2,

                },
                new VillaNumber
                {
                    Villa_Number = 203,
                    VillaId = 3,
                },
                new VillaNumber
                {
                    Villa_Number = 301,
                    VillaId = 1,

                },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 2,

                },
                new VillaNumber
                {
                    Villa_Number = 303,
                    VillaId = 3,
                }
            );
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    VillaId = 1,
                    Name = "Private Pool",
                },
                new Amenity
                {
                    Id = 2,
                    VillaId = 2,
                    Name = "Microwave",
                },
                new Amenity
                {
                    Id = 3,
                    VillaId = 3,
                    Name = "Private Balcony",
                },
                new Amenity
                {
                    Id = 4,
                    VillaId = 1,
                    Name = "Barbecue",
                }
            );
        }
    }
}
