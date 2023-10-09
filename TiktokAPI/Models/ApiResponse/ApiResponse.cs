using System.Net;

namespace TiktokAPI.Models.ApiResponse
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiResponse(string message, int statusCode = 200, object data = null)
        {
            this.Message = message;
            this.StatusCode = (HttpStatusCode)statusCode;
            this.Data = data;
        }
    }
}
