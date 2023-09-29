using System.Net;

namespace KraevedAPI.ClassObjects
{
    [Serializable]
    public class KraevedResponse
    {
        public KraevedResponse(string requestUrl, object? data, string? error, bool status = false, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            RequestUrl = requestUrl;
            Data = data;
            Error = error;
            Status = status;
            StatusCode = httpStatusCode;
        }
        /// <summary>
        /// The Request Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// The Response Data
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// The Response Error
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// The Response Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The Response Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

    }
}
