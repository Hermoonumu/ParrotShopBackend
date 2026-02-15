namespace ParrotShopBackend.Application.Exceptions;

[Serializable]
public class ParrotDoesntExistException : Exception
{
    public ParrotDoesntExistException() : base() { }
    public ParrotDoesntExistException(string msg) : base(msg) { }
    public ParrotDoesntExistException(string msg, Exception innerException) : base(msg, innerException) { }
}