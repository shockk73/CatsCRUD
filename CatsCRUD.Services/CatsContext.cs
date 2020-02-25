using CatsCRUD.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Services.Entites;
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
