namespace Pedalo.UI.Api.Middleware.UnhandledExceptionLogger
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Extension methods for adding the <see cref="UnhandledExceptionLoggerMiddleware"/>.
    /// </summary>
    public static class UnhandledExceptionLoggerMiddlewareExtensions
    {
        /// <summary>
        /// Adds the <see cref="UnhandledExceptionLoggerMiddleware"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The application builder interface.</returns>
        public static IApplicationBuilder UseUnhandledExceptionsLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnhandledExceptionLoggerMiddleware>();
        }
    }
}
