namespace Ecom.Api.Helper
{
    public class ResponseApi
    {
        public ResponseApi(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatuscode(statusCode);
        }
        private string? GetMessageFromStatuscode(int statuscode)
        {
            return statuscode switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                500 => "Internal Server Error",
                _ => null
            };

        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
