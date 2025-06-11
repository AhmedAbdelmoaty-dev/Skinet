namespace Application.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string resourceType,int resourceIdenrifier):
            base($" {resourceType} with Id {resourceIdenrifier} was not found.")
        {
        }
    }
}
