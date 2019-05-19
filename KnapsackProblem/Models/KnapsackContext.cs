using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem.Models
{
    public class KnapsackContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Knapsack> Knapsacks { get; set; }
        public DbSet<KnapsackTask> Tasks { get; set; }

        public KnapsackContext(DbContextOptions<KnapsackContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=KnapsackProblem;Username=efcfuser;Password=efcfuser");
        //}
    }
}
