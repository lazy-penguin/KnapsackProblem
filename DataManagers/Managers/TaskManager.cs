using System.Collections.Generic;
using System.Linq;

namespace DataManagers
{
    public class TaskManager
    {
        public static KnapsackTask New(KnapsackTask task, KnapsackContext context, List<Item> itemList)
        {
            Knapsack knapsack = new Knapsack { KnapsackTask = task, Capacity = task.MaxValue};

            task.MaxValue = 0;
            task.Percent = 0;
            task.Time = 0;
            task.Status = false;
            task.Knapsack = knapsack;
            task.Items = itemList;

            context.Tasks.Add(task);
            context.Knapsacks.Add(knapsack);
            context.SaveChanges();

            return task;
        }

        public static List<Item> GetItems(int id, KnapsackContext context)
        {
            return context.Items.Where(i => i.KnapsackTaskId == id).ToList();
        }
    }
}
