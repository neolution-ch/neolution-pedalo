namespace PedaloWebApp.Core.Converters
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// Converter for Nullable Date Only
    /// </summary>
    public class NullableDateOnlyConverter : ValueConverter<DateOnly?,
      DateTime?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDateOnlyConverter"/> class.
        /// </summary>
        public NullableDateOnlyConverter()
            : base(
            d => d == null
                ? null
                : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
            d => d == null
                ? null
                : new DateOnly?(DateOnly.FromDateTime(d.Value)))
        {
        }
    }
}
