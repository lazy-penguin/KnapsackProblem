using System;
using System.Collections.Generic;
using System.Linq;
using KnapsackProblem.Models;
using KnapsackProblem.Models.Managers;
using Microsoft.AspNetCore.Mvc;

namespace KnapsackProblem.Controllers
{
    public class KnapsackController : Controller
    {
        KnapsackContext Context;
        public KnapsackController(KnapsackContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            return View(Context.Items.ToList());
        }

        public IActionResult Task(int id)
        {
            var task = Context.Tasks.Find(id);
            var items = TaskManager.GetItems(id, Context);
            ViewBag.Task = task;
            return View(items);
        }

        public IActionResult Tasks()
        {
            return View(Context.Tasks.ToList());
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            ViewBag.Item = id;
            List<Item> itemList = new List<Item>();
            itemList.Add(new Item { Weight = 4, Value = 3 });
            itemList.Add(new Item { Weight = 4, Value = 3 });
            return View(itemList);
        }

        [HttpPost]
        public IActionResult New(KnapsackTask task, List<Item> itemList)
        {
            TaskManager.New(task, Context, itemList);
            return View("Tasks", Context.Tasks.ToList());
        }
    }
}