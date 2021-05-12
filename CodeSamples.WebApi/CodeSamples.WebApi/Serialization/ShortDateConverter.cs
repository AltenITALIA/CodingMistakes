using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeSamples.WebApi.Serialization
{
    // HACK: If you don't want to manually insert this converter into every project, you can add the TinyHelpers NuGet Package.
    // It contains a ShortDateConverter with the exact same code.
    public class ShortDateConverter : JsonConverter<DateTime>
    {
        private readonly string serializationFormat;

        public ShortDateConverter() : this(null)
        {
        }

        public ShortDateConverter(string serializationFormat)
            => this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTimeOffset.Parse(reader.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal).Date;

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }
}
