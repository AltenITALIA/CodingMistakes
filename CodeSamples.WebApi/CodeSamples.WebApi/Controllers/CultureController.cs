using System;
using System.Globalization;
using System.Net.Mime;
using CodeSamples.WebApi.Models;
using CodeSamples.WebApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeSamples.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CultureController : ControllerBase
    {
        /// <summary>
        /// This endpoint generates a random number and gets a message from a .RESX file. Unless otherwise specified, the culture of the server is used.
        /// Search for TODO comments about "CULTURE REQUEST LOCALIZATION" to see how to change the culture based on the Accept-Language header.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(CultureSampleResult), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var cultureResult = new CultureSampleResult
            {
                CultureName = CultureInfo.CurrentUICulture.Name,
                Value = new Random().NextDouble() * 10000,
                Message = Messages.WelcomeMessage
            };

            return Ok(cultureResult);
        }
    }
}
