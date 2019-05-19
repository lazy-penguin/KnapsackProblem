using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem.Models
{
    public class KnapsackTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Percent { get; set; }

        [Required]
        public int MaxValue { get; set; }

        public int? Time { get; set; }

        [Required]
        public int KnapsackId { get; set; }
        public Knapsack Knapsack { get; set; }

        public ICollection<Item> Items { get; set; }

        public KnapsackTask()
        {
            Items = new List<Item>();
        }
    }
}
