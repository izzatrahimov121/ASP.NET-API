namespace EduHome.Business.Exceptions;

public sealed class NotFaundException : Exception
{
	public NotFaundException(string? message) : base(message)
	{
	}
}
