using DataManagers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace KnapsackProblem.Utils
{
    public class KnapsackProblemSolver
    {
        public List<Item> Items { private get; set; }
        public KnapsackTask Task { get; private set; }
        public KnapsackContext Context { get; set; }
        public Solve Solve { get; set; }
        public List<Item> PickedItems { get; private set; }

        public CancellationToken Token { private get; set; }
        private int N, W;

        public void Start()
        {
            try
            {
                DynamicSolve();
            }
            finally
            {
                if (Context != null)
                    Context.Dispose();
            }
        }

        public void InitTask(int id)
        {
            var task = Context.Tasks.Find(id);
            Task = task;
            Task.Status = false;
            Solve = task.Solve;
            Items = task.Items.ToList();
        }

        public int CreateNewTask(KnapsackTask task)
        {
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

        private void SaveTable(Stopwatch timer, long prevTime, int[,] table, int k, int w)
        {
            string serializedTable = JsonConvert.SerializeObject(table);
            Solve.Table = serializedTable;
            Solve.K = k;
            Solve.W = w;

            Task.Time = prevTime + timer.ElapsedMilliseconds/1000;
            Task.Percent += (int)((1.0 / (N * W)) * 100);
            Context.SaveChanges();
            Thread.Sleep(2000);
        }

        public void DynamicSolve()
        {
            var timer = Stopwatch.StartNew();
            N = Items.Count();
            W = Task.Knapsack.Capacity;
            int[,] table; 
            int k0, w0;
            long prevTime;

            if (Solve == null)
            {
                table = new int[N+1,W+1];
                Solve = new Solve { KnapsackTask = Task };
                Context.Solves.Add(Solve);
                Context.SaveChanges();
                k0 = 1;
                w0 = 1;
                prevTime = 0;
            }
            else
            {
                table = JsonConvert.DeserializeObject<int[,]>(Task.Solve.Table);
                k0 = Solve.K;
                w0 = Solve.W + 1;
                prevTime = Task.Time;
            }

            for (int k = k0; k <= N; k++)
            {
                for(int w = w0; w <= W; w++)
                {

                    if (Token.IsCancellationRequested)
                        return;

                    if (w >= Items[k - 1].Weight)
                        table[k, w] = Math.Max(table[k - 1, w], table[k - 1, w - Items[k - 1].Weight] + Items[k - 1].Value);
                    else
                        table[k, w] = table[k - 1, w];

                    SaveTable(timer, prevTime, table, k, w);
                }
            }

            timer.Stop();
            Task.Time = timer.ElapsedMilliseconds / 1000;
            Task.MaxValue = table[N, W];

            PickedItems = new List<Item>();
            FindItems(table, N, W);
            Task.Knapsack.Items = PickedItems;

            Task.Percent = 100;
            Task.Status = true;
            Context.SaveChanges();
        }
    }
}
