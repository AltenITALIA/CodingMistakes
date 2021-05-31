namespace CodingMistakes.WebApi.Models
{
    public class CultureSampleResult
    {
        public string CultureName { get; set; }

        public double Value { get; set; }

        public string ValueAsString => Value.ToString();

        public string ValueAsCurrency => Value.ToString("C");

        public string Message { get; set; }
    }
}
