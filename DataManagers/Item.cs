using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagers
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
        public virtual Knapsack Knapsack { get; set; }

        public int? KnapsackTaskId { get; set; }
        public virtual KnapsackTask KnapsackTask { get; set; }
    }
}
