using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingMistakes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MemoryController : ControllerBase
    {
        /// <summary>
        /// This endpoint shows an incorrect way to open a file without disposing it at the end.
        /// </summary>
        [HttpGet("Read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Read(string path)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length);

            return NoContent();
        }

        /// <summary>
        /// This endpoint shows how to correctly open a file inside a using statement. In this way, the file is automatically disposes at the end.
        /// </summary>
        [HttpGet("ReadWithDispose")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ReadWithDispose(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
            }

            return NoContent();
        }
    }
}
