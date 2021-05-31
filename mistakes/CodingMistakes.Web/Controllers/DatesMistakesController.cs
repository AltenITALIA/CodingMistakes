using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CodingMistakes.API.Controllers
{
    [ApiController]
    [Route("api/mistakes/dates")]
    public class DatesMistakesController : ControllerBase
    {
        private readonly ILogger<DatesMistakesController> _logger;

        public DatesMistakesController(ILogger<DatesMistakesController> logger)
        {
            _logger = logger;
        }

        [Route("zone")]
        [HttpGet]
        public IActionResult Zone()
        {
            return Ok(new {
                TimeZone = $"{TimeZoneInfo.Local.DisplayName}",
                Culture = $"{CultureInfo.CurrentCulture.EnglishName}",
                Numbers = $"1{CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator}000{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}00 {CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol}",
                Dates = $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}"
            });
        }

        [Route("parse/bad")]
        [HttpGet]
        public IActionResult DateTimeBadParse(DateTime date)
        {
            return Ok($"{date:F} ({date.Kind})");
        }

        [Route("today")]
        [HttpGet]
        public IActionResult DateTimeToday()
        {
            return Ok(DateTime.Today);
        }
    }
}
