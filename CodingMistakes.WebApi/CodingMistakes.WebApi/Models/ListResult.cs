using System.Collections.Generic;

namespace CodingMistakes.WebApi.Models
{
    public class ListResult<T>
    {
        public long ElapsedMilliseconds { get; set; }

        public IEnumerable<T> Content { get; set; }
    }
}
