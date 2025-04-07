namespace Pedalo.Core.Converters
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// Converter for Date Only
    /// </summary>
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
        /// </summary>
        public DateOnlyConverter()
            : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        {
        }
    }
}
