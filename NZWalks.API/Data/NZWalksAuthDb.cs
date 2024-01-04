using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDb : IdentityDbContext
    {
        public NZWalksAuthDb(DbContextOptions<NZWalksAuthDb> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Roles 

            var readerRoleId = "3ab91dbe-f465-41c7-bdcf-2d6c3bc36471";
            var writerRoleId = "4fee3fbb-5c13-4943-b6a6-f3840042d680";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole()
                {
                    Id= writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },
               
             };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}