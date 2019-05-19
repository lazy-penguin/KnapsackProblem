using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnapsackProblem.Models.Managers
{
    public class TaskManager
    {
        public static void New(KnapsackTask task, KnapsackContext context, List<Item> itemList)
        {
            Knapsack knapsack = new Knapsack { Task = task, Capacity = task.MaxValue };

            task.Knapsack = knapsack;
            task.Items = itemList;

            context.Tasks.Add(task);
            context.Knapsacks.Add(knapsack);
            context.SaveChanges();
        }

        public static List<Item> GetItems(int id, KnapsackContext context)
        {
            return context.Items.Where(i => i.TaskId == id).ToList();
        }
    }
}
