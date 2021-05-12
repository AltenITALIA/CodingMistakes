using System.Diagnostics;
using System.Net.Mime;
using System.Threading.Tasks;
using CodeSamples.BusinessLayer;
using CodeSamples.DataAccessLayer.Entities;
using CodeSamples.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeSamples.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class DatabaseController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public DatabaseController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        /// <summary>
        /// This endpoints shows how to get a single item from an Entity Framework Core query written in LINQ. For this example, the AdventureWorks database is used.
        /// Search for HACK comments in the TransactionService.cs file for details.
        /// </summary>
        [HttpGet("transactions/{id:int}")]
        [ProducesResponseType(typeof(TransactionItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await transactionService.GetAsync(id);
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }

        /// <summary>
        /// This endpoints shows how to correctly use LINQ when querying a database. For this example, the AdventureWorks database is used.
        /// Try to execute this method a couple of times to be sure that the context has been correctly loaded and then check the elapsedMilliseconds value in the reponse.
        /// Search for HACK comments in the TransactionService.cs file for details.
        /// </summary>
        [HttpGet("products/{id:int}/transactions")]
        [ProducesResponseType(typeof(ListResult<TransactionItem>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTransactionsByProductId(int id)
        {
            var stopwatch = Stopwatch.StartNew();

            var transactions = await transactionService.GetByProductIdAsync(id);

            stopwatch.Stop();

            var result = new ListResult<TransactionItem>
            {
                Content = transactions,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            return Ok(result);
        }

        /// <summary>
        /// This endpoints shows an incorrect use of LINQ when querying a database, causing all the table to be gathered before applying the WHERE condition. For this example, the AdventureWorks database is used.
        /// Try to execute this method a couple of times to be sure that the context has been correctly loaded and then check the elapsedMilliseconds value in the reponse.
        /// Search for HACK comments in the TransactionService.cs file for details.
        /// </summary>
        [HttpGet("inmemory/products/{id:int}/transactions")]
        [ProducesResponseType(typeof(ListResult<TransactionItem>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> InMemoryGetTransactionsByProductId(int id)
        {
            var stopwatch = Stopwatch.StartNew();

            var transactions = await transactionService.InMemoryGetByProductIdAsync(id);

            stopwatch.Stop();

            var result = new ListResult<TransactionItem>
            {
                Content = transactions,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            return Ok(result);
        }
    }
}
