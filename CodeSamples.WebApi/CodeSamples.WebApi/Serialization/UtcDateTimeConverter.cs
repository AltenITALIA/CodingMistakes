using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeSamples.WebApi.Serialization
{
    // HACK: If you don't want to manually insert this converter into every project, you can add the TinyHelpers NuGet Package.
    // It contains an UtcDateTimeConverter with the exact same code.
    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string serializationFormat;

        public UtcDateTimeConverter(string serializationFormat = null)
            => this.serializationFormat = serializationFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.Parse(reader.GetString()).ToUniversalTime();

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue((value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value)
                .ToString(serializationFormat));
    }
}
