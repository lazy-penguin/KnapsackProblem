using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem.Models
{
    public class Knapsack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Capacity { get; set; }

        public KnapsackTask Task { get; set; }

        public ICollection<Item> Items { get; set; }
        public Knapsack()
        {
            Items = new List<Item>();
        }
    }
}
