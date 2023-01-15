namespace EduHome.Business.Exceptions;

public sealed class NotFoundException : Exception
{
	public NotFoundException(string? message) : base(message)
	{
	}
}
