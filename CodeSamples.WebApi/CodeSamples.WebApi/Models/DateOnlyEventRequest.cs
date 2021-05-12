using System;

namespace CodeSamples.WebApi.Models
{
    public class DateOnlyEventRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        // TODO: uncomment this line to properly support DATE ONLY PROPERTY.
        //[System.Text.Json.Serialization.JsonConverter(typeof(ShortDateConverter))]
        public DateTime Date { get; set; }
    }
}
