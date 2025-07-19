

namespace Application.Exceptions
{
    public class ForbiddenException:AppException
    {
        public ForbiddenException(string message="Forbidden")
            : base(403, message) { }

    }
}
