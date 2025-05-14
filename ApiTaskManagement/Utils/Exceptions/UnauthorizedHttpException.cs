namespace ApiTaskManagement.Utils.Exceptions
{
    public class UnauthorizedHttpException : HttpException
    {
        public UnauthorizedHttpException(string message = "Unauthorized")
            : base(message, StatusCodes.Status401Unauthorized) { }
    }
}
