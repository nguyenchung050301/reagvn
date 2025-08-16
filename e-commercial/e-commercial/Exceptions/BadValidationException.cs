namespace e_commercial.Exceptions
{
    public class BadValidationException : Exception
    {
        public string FieldName { get; set; }
        public BadValidationException(string message, string fieldName) : base(message)  => FieldName = fieldName;
        public BadValidationException(string message) : base(message) => FieldName = message;

    }
}
