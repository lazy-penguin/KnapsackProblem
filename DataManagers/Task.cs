using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagers
{
    public class KnapsackTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Percent { get; set; }

        [Required]
        public int MaxValue { get; set; }

        [Required]
        public long Time { get; set; }

        public bool? Status { get; set; }

        [Required]
        public int KnapsackId { get; set; }
        public virtual Knapsack Knapsack { get; set; }

        public int? SolveId { get; set; }
        public virtual Solve Solve { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public KnapsackTask()
        {
            Items = new List<Item>();
        }
    }
}
