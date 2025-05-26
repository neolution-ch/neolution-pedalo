namespace PedaloWebApp.UI.Api.Middleware.UnhandledExceptionLogger
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Middleware to ensure unhandled exceptions will be logged.
    /// </summary>
    public class UnhandledExceptionLoggerMiddleware
    {
        /// <summary>
        /// The next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionLoggerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public UnhandledExceptionLoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Executes the middleware and will catch unhandled exceptions that happen later in the request pipeline.
        /// It ensures such an exceptions is logged and then rethrows it to ensure the expected exception flow.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task InvokeAsync(HttpContext context, ILogger<UnhandledExceptionLoggerMiddleware> logger)
        {
            if (context == null)
            {
                logger.LogCritical("The HttpContext was null when {middleware} was invoked", nameof(UnhandledExceptionLoggerMiddleware));
                throw new ArgumentNullException(nameof(context));
            }

            return this.InvokeInternalAsync(context, logger);
        }

        /// <summary>
        /// Internal async call of <see cref="InvokeAsync"/>
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task InvokeInternalAsync(HttpContext context, ILogger<UnhandledExceptionLoggerMiddleware> logger)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                // Needed if NLog layout renderer ${aspnet-request-posted-body} is used
                // https://github.com/NLog/NLog/wiki/AspNet-Request-posted-body-layout-renderer
                // https://medium.com/@rm.dev/re-reading-asp-net-core-request-bodies-with-enablebuffering-987d8668bf85
                context.Request.EnableBuffering();

                logger.LogCritical(ex, "An unhandled exception has occurred while executing the request");
                throw;
            }
        }
    }
}
