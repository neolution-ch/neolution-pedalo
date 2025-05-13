namespace PedaloWebApp.UI.Api.Endpoints.Errors
{
    /// <inheritdoc/>
    [AllowAnonymous]
    public class ThrowError : EndpointBaseSync.WithRequest<ThrowErrorRequest>.WithActionResult<bool>
    {
        /// <summary>
        /// Throws an error. This is just used as for error handling demonstration purposes and can be deleted in a real world application.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The requested error</returns>
        [HttpPost("/[baseroute]/{Code}")]
        public override ActionResult<bool> Handle([FromMultiSource] ThrowErrorRequest request)
        {
            if (request is null)
            {
                return this.BadRequest("Request cannot be null");
            }

            return this.StatusCode(request.Code, request.ErrorMessage);
        }
    }
}
