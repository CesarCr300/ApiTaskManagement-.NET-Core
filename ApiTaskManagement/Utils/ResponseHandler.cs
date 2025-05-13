namespace ApiTaskManagement.Utils
{
    public class ResponseHandler
    {
        public static Response<T> Success<T>(T data, string message = "Success", int status = 200)
        {
            return new Response<T>
            {
                Message = message,
                Status = status,
                Data = data
            };
        }

        public static Response<object> Success(string message = "Success", int status = 200)
        {
            return new Response<object>
            {
                Message = message,
                Status = status,
                Data = null
            };
        }

        public static Response<object> Error(string message, int status = 500)
        {
            return new Response<object>
            {
                Message = message,
                Status = status,
                Data = null
            };
        }
    }

    public class Response<T>
    {
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public T? Data { get; set; }
    }

}
