using System.Collections.Generic;
using System.Linq;
using DataManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using KnapsackProblem.Utils;

namespace KnapsackProblem.Controllers
{
    public class KnapsackController : Controller
    {
        private readonly KnapsackContext Context;
        private readonly IHostedService FingUnfinishedService;
        private readonly TaskService TaskService;
        public KnapsackController(KnapsackContext context, IHostedService fingUnfinishedService, TaskService taskService)
        {
            Context = context;
            FingUnfinishedService = fingUnfinishedService;
            TaskService = taskService;
        }

        [HttpGet]
        public IActionResult Stop(int id)
        {
            TaskService.Interrupt(Context, id);
            return RedirectToAction("Tasks");
        }

        [HttpGet]
        public IActionResult Continue(int id)
        {
            TaskService.Restart(id);
            return RedirectToAction("Tasks");
        }

        [HttpGet]
        public IActionResult Task(int id)
        {
            var task = Context.Tasks.Find(id);
            ViewBag.Task = task;
            var table = JsonConvert.DeserializeObject<int[,]>(task.Solve.Table);
            ViewBag.Table = table;
            return View();
        }

        [HttpGet]
        public IActionResult Tasks()
        {
            return View(Context.Tasks.OrderBy(e => e.Status.GetValueOrDefault() ? 1 : 0).ToList());
        }

        [HttpGet]
        public IActionResult New(int i)
        {
            var itemList = new List<Item>();
            
            for (int j = 1; j <= i; j++)
                itemList.Add(new Item { Weight = 4, Value = 3 });
            return View(itemList);
        }

        [HttpGet]
        public IActionResult Start()
        {
            return RedirectToAction("New", new { i = 5 });
        }

        [HttpPost]
        public IActionResult New(KnapsackTask task, List<Item> itemList)
        {
            TaskService.Start(task, itemList);
            return RedirectToAction("Tasks");
        }
    }
}