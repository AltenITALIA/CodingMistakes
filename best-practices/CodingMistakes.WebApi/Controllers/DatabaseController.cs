using System.Diagnostics;
using System.Net.Mime;
using System.Threading.Tasks;
using CodingMistakes.BusinessLayer;
using CodingMistakes.DataAccessLayer.Entities;
using CodingMistakes.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingMistakes.WebApi.Controllers
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
        /// Try with an ID like "100000", "100001" and so on.
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
        /// Try with an ID like "888", "900", "901" and execute this method a couple of times to be sure that the context has been correctly loaded and then check the elapsedMilliseconds value in the reponse.
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
        /// Try with an ID like "888", "900", "901" and execute this method a couple of times to be sure that the context has been correctly loaded and then check the elapsedMilliseconds value in the reponse.
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

        /// <summary>
        /// This endpoints shows the issue that is caused when we have an async void call, so that we can't await it:
        /// The execution can't wait for the task to complete, so it continue with the next istrunctions, causing a runtime exception.
        /// Try with an ID like "100000", "100001" and so on.
        /// Search for HACK comments in the TransactionService.cs file for details.
        /// </summary>
        [HttpGet("transactions/{id:int}/AsyncVoid")]
        [ProducesResponseType(typeof(TransactionItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTransactionWithAsyncVoid(int id)
        {
            // Since this method does not returns a Task, it is impossibile to await it.
            transactionService.LoadAll();

            var transaction = await transactionService.GetAsync(id);
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }
    }
}
