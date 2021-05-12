using System;
using System.Net.Mime;
using CodingMistakes.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingMistakes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class DateController : ControllerBase
    {
        /// <summary>
        /// THE PROBLEM: if you decide to handle all dates as UTC, as the best practices suggest, in case you have a DateTime field that contains 
        /// only a date (i.e., "2021-05-12"), it could be converted to a different value according to the timezone (because, if no time has been specified, it is automatically assumed that is 00:00:00).
        /// After adding support for UTC dates (refer to the comments with name "UTC DATE TIME ZONE HANDLING"), try to pass a value of "2021-05-12" in the "date" property of the body and see the response.
        /// THE SOLUTION: you should apply a "ShortDateConverter" to the property that contains only a date.
        /// Search for TODO comments about "DATE ONLY PROPERTY" to see how to add it.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DateInformation), StatusCodes.Status200OK)]
        public IActionResult Post(DateOnlyEventRequest request)
        {
            var dateInformation = new DateInformation
            {
                Date = request.Date,
                TimeZone = TimeZoneInfo.Local.Id
            };

            return Ok(dateInformation);
        }
    }
}
