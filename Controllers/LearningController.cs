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
    public class LearningController : Controller
    {
        private readonly ILogger<LearningController> _logger;
        private IWordService ws;

        public LearningController(ILogger<LearningController> logger, IWordService wordService)
        {
            _logger = logger;
            ws = wordService;
        }

        [HttpGet("Index")]
        public IActionResult Index() => View();

        [HttpPost("Index")]
        public IActionResult Index(int id, bool successful)
        {
            ws.ChangePoints(id, successful);
            return Ok();
        }

        [HttpGet("LoadWords")]
        public ActionResult LoadWords() => Json(ws.GetWords());
    }
}
