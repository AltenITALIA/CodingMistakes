using System;

namespace CodingMistakes.WebApi.Models
{
    public class DateOnlyInformation
    {
        // TODO: uncomment this line to properly support DATE ONLY PROPERTY.
        //[System.Text.Json.Serialization.JsonConverter(typeof(ShortDateConverter))]
        public DateTime Date { get; set; }

        public string TimeZone { get; set; }
    }
}
