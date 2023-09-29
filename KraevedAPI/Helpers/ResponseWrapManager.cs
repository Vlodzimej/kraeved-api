using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using KraevedAPI.ClassObjects;
using KraevedAPI.Constants;

namespace KraevedAPI.Helpers
{
    /// <summary>
    /// Response Wrap Manager to handle any customizations on result and return Custom Formatted Response.
    /// </summary>
    public static class ResponseWrapManager
    {
        /// <summary>
        /// The Response Wrapper method handles customizations and generate Formatted Response.
        /// </summary>
        /// <param name="result">The Result</param>
        /// <param name="context">The HTTP Context</param>
        /// <param name="exception">The Exception</param>
        /// <returns>Sample Response Object</returns>
        public static KraevedResponse ResponseWrapper(object? result, HttpContext context, object? exception = null)
        {
            var requestUrl = context.Request.GetDisplayUrl();
            var data = result;
            var error = exception != null ? ServiceConstants.ExceptionWrapperMessage : null;
            var status = result != null;
            var httpStatusCode = (HttpStatusCode)context.Response.StatusCode;

            // NOTE: Add any further customizations if needed here

            return new KraevedResponse(requestUrl, data, error, status, httpStatusCode);
        }
    }
}
