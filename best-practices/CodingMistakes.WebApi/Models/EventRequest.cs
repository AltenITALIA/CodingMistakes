using System;

namespace CodingMistakes.WebApi.Models
{
    public class EventRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
