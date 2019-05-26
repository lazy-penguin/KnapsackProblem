using DataManagers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KnapsackProblem.Controllers
{
    public class KnapsackProblemSolver
    {
        public List<Item> Items { get; set; }
        public KnapsackTask Task { get; private set; }
        public KnapsackContext Context { get; set; }
        public Solve Solve { get; set; }
        public List<Item> PickedItems { get; private set; }

        private int N, W;

        public void InitContext()
        {
            if (Context == null)
                return;

            var optionsBuilder = new DbContextOptionsBuilder<KnapsackContext>();
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(Context.Database.GetDbConnection().ConnectionString);
            Context = new KnapsackContext(optionsBuilder.Options);
        }

        public void InitTask(int id)
        {
            var task = Context.Tasks.Find(id);
            Task = task;
            Solve = task.Solve;
            Items = task.Items.ToList();
        }

        public int CreateNewTask(KnapsackTask task)
        {
            InitContext();
            Task = TaskManager.New(task, Context, Items);
            return Task.Id;
        }

        private void FindItems(int[,] table, int k, int s)
        {
            if (table[k, s] == 0)
                return;
            if (table[k - 1, s] == table[k, s])
                FindItems(table, k - 1, s);
            else
            {
                FindItems(table, k - 1, s- Items[k - 1].Weight);
                var item = Items[k-1];
                PickedItems.Add(item);
            }
        }

        private void SaveTable(int[,] table, int k, int w)
        {
            string serializedTable = JsonConvert.SerializeObject(table);
            Solve.Table = serializedTable;
            Solve.K = k;
            Solve.W = w;

            Task.Percent += (int)((1.0 / (N * W)) * 100);
            Context.SaveChanges();
            Thread.Sleep(2000);
        }

        public void DynamicSolve(CancellationToken token)
        {
            N = Items.Count();
            W = Task.Knapsack.Capacity;
            var table = new int[N + 1, W + 1];
            int k0, w0;

            if (Solve == null)
            {
                Solve = new Solve { KnapsackTask = Task };
                Context.Solves.Add(Solve);
                Context.SaveChanges();
                k0 = 1;
                w0 = 1;
            }
            else
            {
                k0 = Solve.K;
                w0 = Solve.W;
            }

            for (int k = k0; k <= N; k++)
            {
                for(int w = w0; w <= W; w++)
                {

                    if (token.IsCancellationRequested)
                    {
                        Context.Dispose();
                        return;
                    }

                    if (w >= Items[k - 1].Weight)
                        table[k, w] = Math.Max(table[k - 1, w], table[k - 1, w - Items[k - 1].Weight] + Items[k - 1].Value);
                    else
                        table[k, w] = table[k - 1, w];

                    SaveTable(table, k, w);
                }
            }

            Task.MaxValue = table[N, W];

            PickedItems = new List<Item>();
            FindItems(table, N, W);
            Task.Knapsack.Items = PickedItems;

            Task.Percent = 100;
            Task.Status = true;
            Context.SaveChanges();
            Context.Dispose();
        }
    }
}
