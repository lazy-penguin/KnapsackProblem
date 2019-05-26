using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataManagers
{
    public class Solve
    {
        [Key]
        public int Id { get; set; }

        public string Table { get; set; }
        public int W { get; set; }
        public int K { get; set; }

    public virtual KnapsackTask KnapsackTask { get; set; }

    }
}
