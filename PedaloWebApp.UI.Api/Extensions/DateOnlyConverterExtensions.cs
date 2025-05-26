namespace PedaloWebApp.UI.Api.Extensions
{
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The Date Only Converter Extensions
    /// </summary>
    public static class DateOnlyConverterExtensions
    {
        /// <summary>
        /// The date only regular expression
        /// </summary>
        private static readonly Regex DateOnlyRegex = new("^(\\d\\d\\d\\d)-(\\d\\d)-(\\d\\d)(T |\\s |\\z)", RegexOptions.Compiled);

        /// <summary>
        /// Adds the date only converters.
        /// </summary>
        /// <param name="options">The options.</param>
        public static void AddDateOnlyConverters(this JsonSerializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.Converters.Add(new DateOnlyConverter());
            options.Converters.Add(new DateOnlyNullableConverter());
        }

        /// <summary>
        /// Parses the date to <see cref="DateOnly"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="DateOnly"/>.</returns>
        private static DateOnly? ParseDateOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var match = DateOnlyRegex.Match(value);
            return match.Success
                ? new DateOnly(
                    int.Parse(match.Groups[1].Value, NumberFormatInfo.InvariantInfo),
                    int.Parse(match.Groups[2].Value, NumberFormatInfo.InvariantInfo),
                    int.Parse(match.Groups[3].Value, NumberFormatInfo.InvariantInfo))
                : default;
        }

        /// <summary>
        /// The Date Only Nullable Converter
        /// </summary>
        private sealed class DateOnlyNullableConverter : JsonConverter<DateOnly?>
        {
            /// <inheritdoc/>
            public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TryGetDateTime(out var dt))
                {
                    return DateOnly.FromDateTime(dt);
                }

                var value = reader.GetString();
                return value is not null ? ParseDateOnly(value) : null;
            }

            /// <inheritdoc/>
            public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
                => writer.WriteStringValue(value?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The Date Only Converter
        /// </summary>
        private sealed class DateOnlyConverter : JsonConverter<DateOnly>
        {
            /// <inheritdoc/>
            public override DateOnly Read(
                    ref Utf8JsonReader reader,
                    Type typeToConvert,
                    JsonSerializerOptions options)
            {
                if (reader.TryGetDateTime(out var dt))
                {
                    return DateOnly.FromDateTime(dt);
                }

                var value = reader.GetString();
                if (value is null)
                {
                    return default;
                }

                return ParseDateOnly(value) ?? default;
            }

            /// <inheritdoc/>
            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
    }
}
