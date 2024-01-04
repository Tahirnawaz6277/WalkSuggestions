using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulty 
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("e2f5c606-070e-45fb-b3ed-1fe0e6b7827f"),
                    Name = "Easy"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("0ff597e9-b117-4168-8d0f-f26658ef1b59"),
                    Name = "Meduim"
                },
                    new Difficulty()
                {
                    Id = Guid.Parse("4f364ded-7401-48f3-9bc5-36c33a111286"),
                    Name = "Hard"
                }

            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Sedding for region

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("417da255-4f11-4bdc-9e17-31a051a9ff91"),
                    Code = "NZ",
                    Name = "Newzeland",
                    RegionImageUrl = "Nweland.JPG"
                },
                new Region()
                {
                    Id = Guid.Parse("da9b0b1f-1cf0-410c-a932-4ce704b200e9"),
                    Code = "NL",
                    Name = "NatherLand",
                    RegionImageUrl = "NatherLand.JPG"
                },
                new Region()
                {
                    Id = Guid.Parse("8fc7f98e-df80-4941-a808-6f999b126bfb"),
                    Code = "JP",
                    Name = "Japan",
                    RegionImageUrl = "Japan.JPG"
                }
,
                new Region()
                {
                    Id = Guid.Parse("29f23766-a9c7-4c52-a62b-8ee629731b15"),
                    Code = "KPK",
                    Name = "Kherber Pakhton khwa",
                    RegionImageUrl = "Kpk.JPG"
                }

            };
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
