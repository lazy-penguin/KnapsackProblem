using System.Collections.Generic;
using System.Linq;
using DataManagers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KnapsackProblem.Controllers
{
    public class KnapsackController : Controller
    {
        private readonly KnapsackContext Context;
        public KnapsackController(KnapsackContext context)
        {
            Context = context;
        }

        public IActionResult Stop(int id)
        {
            TaskController.Interrupt(id);
            return RedirectToAction("Tasks");
        }

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
            return View(Context.Tasks.ToList());
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            ViewBag.Item = id;
            var itemList = new List<Item>
            {
                new Item { Weight = 4, Value = 3 },
                new Item { Weight = 4, Value = 3 },
                new Item { Weight = 4, Value = 3 },
                new Item { Weight = 4, Value = 3 },
                new Item { Weight = 4, Value = 3 }
            };
            return View(itemList);
        }

        public IActionResult Start()
        {
            TaskController.FindUnfinished(Context);
            return RedirectToAction("New");
        }

        [HttpPost]
        public IActionResult New(KnapsackTask task, List<Item> itemList)
        {
            TaskController.Start(Context, task, itemList);
            return RedirectToAction("Tasks");
        }
    }
}