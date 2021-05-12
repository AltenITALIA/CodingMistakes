using System;
using System.Globalization;
using System.Net.Mime;
using CodingMistakes.WebApi.Models;
using CodingMistakes.WebApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingMistakes.WebApi.Controllers
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

        /// <summary>
        /// This endpoint shows how the server parses a number that is contained in a string property without specifying a culture. In this case, the culture of the server is used.
        /// Try to use "." or "," as decimal separator as see the result.
        /// </summary>
        [HttpPost("parseerror")]
        [ProducesResponseType(typeof(CultureSampleResult), StatusCodes.Status200OK)]
        public IActionResult ParseError(CultureRequest request)
        {
            var cultureResult = new CultureSampleResult
            {
                CultureName = CultureInfo.CurrentUICulture.Name,
                Value = double.Parse(request.Number)
            };

            return Ok(cultureResult);
        }

        /// <summary>
        /// This endpoint shows how the correctly parse a number that is contained in a string property using the Invariant Culture, so each decimal separator is accepted,
        /// no matter the culture of the server.
        /// You can use both "." or "," as decimal separator.
        /// </summary>
        [HttpPost("parseok")]
        [ProducesResponseType(typeof(CultureSampleResult), StatusCodes.Status200OK)]
        public IActionResult ParseOK(CultureRequest request)
        {
            var cultureResult = new CultureSampleResult
            {
                CultureName = CultureInfo.CurrentUICulture.Name,
                // HACK: this parse method tipically should be placed in an extension method.
                Value = double.Parse(request.Number.Replace(",", "."), CultureInfo.InvariantCulture)
            };

            return Ok(cultureResult);
        }
    }
}
