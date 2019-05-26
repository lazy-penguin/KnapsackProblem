using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagers
{
    public class KnapsackContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Knapsack> Knapsacks { get; set; }
        public DbSet<KnapsackTask> Tasks { get; set; }
        public DbSet<Solve> Solves { get; set; }

        public KnapsackContext(DbContextOptions<KnapsackContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
