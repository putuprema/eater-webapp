using System.Net;

namespace EaterWebClient.Exceptions
{
    public class EaterApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public EaterApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
