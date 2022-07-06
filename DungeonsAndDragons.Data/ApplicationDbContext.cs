using DungeonsAndDragons.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Classes> Classes { get; set; }
    }
}