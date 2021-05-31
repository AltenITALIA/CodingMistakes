using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingMistakes.API.Controllers
{
    [ApiController]
    [Route("api/mistakes/numbers")]
    public class NumbersMistakesController : ControllerBase
    {
        private readonly ILogger<NumbersMistakesController> _logger;

        public NumbersMistakesController(ILogger<NumbersMistakesController> logger)
        {
            _logger = logger;
        }

        [Route("parse/bad")]
        [HttpGet]
        public IActionResult DecimalBadParse(decimal number)
        {
            return Ok(number);
        }
    }
}
