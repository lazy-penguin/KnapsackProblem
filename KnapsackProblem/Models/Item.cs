using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int Value { get; set; }

        public int? KnapsackId { get; set; }
        public Knapsack Knapsack { get; set; }

        public int? TaskId { get; set; }
        public KnapsackTask Task { get; set; }
    }
}
