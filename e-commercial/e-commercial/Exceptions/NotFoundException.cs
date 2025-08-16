namespace e_commercial.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string Id) : base($"Item with ID {Id} not found.") { }

    }
}
