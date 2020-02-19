using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Models
{
    public class CatsContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public CatsContext(DbContextOptions<CatsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
