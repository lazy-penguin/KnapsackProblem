using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagers
{
    public class Knapsack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Capacity { get; set; }

        public virtual KnapsackTask KnapsackTask { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public Knapsack()
        {
            Items = new List<Item>();
        }
    }
}
