using KraevedAPI.Helpers;
using Newtonsoft.Json;

namespace KraevedAPI.Core
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            // Storing Context Body Response
            var currentBody = context.Response.Body;

            // Using MemoryStream to hold Controller Response
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // Passing call to Controller
            await _next(context);

            // Resetting Context Body Response
            context.Response.Body = currentBody;

            // Setting Memory Stream Position to Beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read Memory Stream data to the end
            var readToEnd = new StreamReader(memoryStream).ReadToEnd();

            // Deserializing Controller Response to an object
            var result = JsonConvert.DeserializeObject(readToEnd);
            var exception = ""; // Exception Caught

            // Invoking Customizations Method to handle Custom Formatted Response
            var response = ResponseWrapManager.ResponseWrapper(result, context, exception);

            // return response to caller
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
