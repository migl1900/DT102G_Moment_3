using DT102G_Moment3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_Moment3.Data
{
    public class CollectionContext : DbContext
    {
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {

        }

        // Create database tables
        public DbSet<Record> Records { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Lending> Lendings { get; set; }
    }
}
