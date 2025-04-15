namespace PedaloWebApp.UI.Api.UnitTests.Customizations
{
    using System.Security.Cryptography;

    /// <summary>
    /// AutoFixture Customization for <see cref="DateOnly"/> types.
    /// </summary>
    /// <seealso cref="ICustomization" />
    public class DateOnlyCustomization : ICustomization
    {
        /// <summary>
        /// Customizes the specified fixture.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            ArgumentNullException.ThrowIfNull(fixture);

            var startDate = DateOnly.FromDateTime(DateTime.UnixEpoch);
            var endDate = DateOnly.FromDateTime(DateTime.Today);

            var startDayNumber = startDate.DayNumber;
            var endDayNumber = endDate.DayNumber;

            var randomDayNumber = GenerateRandomNumber(startDayNumber, endDayNumber);

            fixture.Customize<DateOnly>(c => c.FromFactory(() => DateOnly.FromDayNumber(randomDayNumber)));
        }

        /// <summary>
        /// Generates the random number.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The random number.</returns>
        private static int GenerateRandomNumber(int minValue, int maxValue)
        {
            var randomNumber = new byte[4];
            RandomNumberGenerator.Fill(randomNumber);
            var value = BitConverter.ToInt32(randomNumber, 0);
            return Math.Abs(value % (maxValue - minValue + 1)) + minValue;
        }
    }
}
