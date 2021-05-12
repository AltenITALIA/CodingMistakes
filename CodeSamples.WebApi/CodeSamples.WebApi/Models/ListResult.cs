using System.Collections.Generic;

namespace CodeSamples.WebApi.Models
{
    public class ListResult<T>
    {
        public long ElapsedMilliseconds { get; set; }

        public IEnumerable<T> Content { get; set; }
    }
}
