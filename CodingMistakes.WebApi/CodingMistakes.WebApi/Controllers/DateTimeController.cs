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
    public class DateTimeController : ControllerBase
    {
        /// <summary>
        /// This endpoints gets the current date and time in the timezone of the server.
        /// For example, if the project is deployed on premise in a server in Italy, you get a date like "2021-05-11T11:06:26.31969+02:00"
        /// If, instead, you publish the application on Azure, the timezone will be UTC and you'll get a date like "2021-05-11T09:06:26.31969+00:00"
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DateInformation), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var dateInformation = new DateInformation
            {
                Date = DateTime.Now,
                TimeZone = TimeZoneInfo.Local.Id
            };

            return Ok(dateInformation);
        }

        /// <summary>
        /// THE PROBLEM: when the backend receives a date like "2021-05-11T11:06:26.31969+02:00" (i.e., a local date) it converts it in its timezone.
        /// If the server uses the same timezone of the client, the received value is the same of the passed one.
        /// If, instead, the server is in another timezone, it will receive a value that is different from the passed one (becaused, as said, it will be automatically converted to the server timezone).
        /// So, if this value is saved to a database in a datetime field, the value on the database will be different from the one that the client sent.
        /// As a consequence, when we retrieve the stored value, we get a date in the timezone of the server, but the client doesn't know (and mustn't know) what this timezone is, so it is unable to handle it correctly.
        /// The SOLUTION is that every datetime is handled as UTC, on both client and server, so it will always possible to deal with it correctly, no matter the actual timezone.
        /// Search for TODO comments about "UTC DATE TIME ZONE HANDLING" to see how to force the use of UTC format.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DateInformation), StatusCodes.Status200OK)]
        public IActionResult Post(EventRequest request)
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
