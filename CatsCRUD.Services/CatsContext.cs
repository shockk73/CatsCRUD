using CatsCRUD.Services.Entites;
using CatsCRUD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CatsCRUD.Services
{
    public class CatsContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public DbSet<User> Users { get; set; }

        public CatsContext(DbContextOptions<CatsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
