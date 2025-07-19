namespace Application.Exceptions
{
    public class UnAuthorizedException:AppException
    {
        public UnAuthorizedException(string message="unAuthorized")
            : base(401,message) { }
    }
}
