using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vocabulary.Controllers
{
    [Route("[controller]")]
    public class WordsController : Controller
    {
        private readonly ILogger<WordsController> _logger;
        private IWordService ws;

        public WordsController(ILogger<WordsController> logger, IWordService wordService)
        {
            _logger = logger;
            ws = wordService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Word> words = ws.GetAll();
            ViewBag.Count = words.Count();
            // for (int i = 1; i <= 5; ++i)
            //     ViewData[i.ToString()] = words.Where(x => x.Points == i).Count() * 100 / words.Count();
            ViewBag.One = words.Where(x => x.Points == 1).Count() * 100 / words.Count();
            ViewBag.Two = words.Where(x => x.Points == 2).Count() * 100 / words.Count();
            ViewBag.Three = words.Where(x => x.Points == 3).Count() * 100 / words.Count();
            ViewBag.Four = words.Where(x => x.Points == 4).Count() * 100 / words.Count();
            ViewBag.Five = words.Where(x => x.Points == 5).Count() * 100 / words.Count();
            return View(words);
        }

        [HttpGet("[action]")]
        public IActionResult Create() => View();

        [HttpPost("[action]")]
        public IActionResult Create(Word word)
        {
            if (ModelState.IsValid && IsNotDuplicate(word))
            {
                ws.Add(word);
                return RedirectToAction("Show", new { word.Id });
            }
            else
            {
                return View(word);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Show(int id) => View(ws.Find(id));

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id) => View(ws.Find(id));

        [HttpPost("Edit")]
        public IActionResult Edit(Word word)
        {
            if (ModelState.IsValid)
            {
                ws.Update(word);
                return RedirectToAction("Show", new { word.Id });
            }
            else
            {
                return View(word);
            }
        }

        [HttpPost("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            ws.Destroy(id);
            return RedirectToAction("Index");
        }

        [HttpPost("[action]")]
        public IActionResult SetLearnStatus(int id, bool isLearned)
        {
            Word word = ws.Find(id);
            word.IsLearned = isLearned;
            ws.Update(word);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Statistic()
        {
            WordStatistic stat = ws.Statistic();
            return Ok(new {
                count = stat.Count,
                groups = stat.Groups
            });
        }

        [NonAction]
        private bool IsNotDuplicate(Word word) => !ws.GetAll().Any(w => w.Eng == word.Eng);
    }
}
