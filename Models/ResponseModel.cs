namespace MobileStats_WebAPIs.Models
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ResponseModel(int statusCode, string message, T? data)
        {
            StatusCode = statusCode == 0 ? 200 : statusCode;
            Message = !string.IsNullOrEmpty(message) ? message : "Success";
            Data = data;
        }
    }

}
