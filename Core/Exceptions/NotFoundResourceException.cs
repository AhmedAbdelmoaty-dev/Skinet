namespace Application.Exceptions
{
    public class NotFoundResourceException:AppException
    {
        public NotFoundResourceException(string resourceType,int resourceIdenrifier):
            base(400,$" {resourceType} with Id {resourceIdenrifier} was not found.")
        {
        }
    }

}
