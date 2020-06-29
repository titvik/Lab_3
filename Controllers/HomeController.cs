using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using lab3.Models;
using System.ComponentModel.DataAnnotations;
using lab3.Utilities;

namespace lab3.Controllers
{
    public class HomeController : Controller
    {
        Random rnd = new Random();
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
          
            return View();
        }

        public IActionResult InitQuiz()
        {
            HttpContext.Session.Clear();
            List<String> tasks = new List<string>();

            HttpContext.Session.SetObj("SessionTasks", tasks);
            TempData["RightAnswerCount"] = 0;

            return RedirectToAction("Quiz");
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            List<String> tasks = HttpContext.Session.GetObj<List<String>>("SessionTasks");
            int A = rnd.Next(10);
            int B = rnd.Next(10);
            String op = "";
            if (rnd.Next(0,2)  == 0)
            {
                op = "+";
                TempData["RightAnswer"] = A+B;
            }
            else {
                op = "-";
                TempData["RightAnswer"] = A-B;
            }

            String task = A + op + B;
            TempData["curr_task"] = task;
            
            tasks.Add(task);
            
            
            HttpContext.Session.SetObj("SessionTasks", tasks);

            return View();
        }


        [HttpPost]
        public IActionResult Quiz(string btn, SolutionModel sm)
        {
            if (Convert.ToInt32(sm.solution) == 13)
            {
                ModelState.AddModelError("", "Число 13 - несчастливое число, выберите другое :)");
            }
            if (ModelState.IsValid)
            {
                int right_ans = (int)TempData["RightAnswer"];
                int right_ans_count = (int)TempData["RightAnswerCount"];
                    
                    
                List<String> tasks = HttpContext.Session.GetObj<List<String>>("SessionTasks");
                String sol = sm.solution.ToString();
                tasks.Add(sol);

                HttpContext.Session.SetObj("SessionTasks", tasks);

                if (right_ans == Convert.ToInt32(sm.solution))
                {
                    right_ans_count++;
                    TempData["RightAnswerCount"] = right_ans_count;
                }
                    

                if (btn == "next")
                    return RedirectToAction("Quiz");

                if (btn == "finish")
                    return View("Results");
            }
            else
                return View();


            return BadRequest(500);
        }

        public IActionResult Results()
        {
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
