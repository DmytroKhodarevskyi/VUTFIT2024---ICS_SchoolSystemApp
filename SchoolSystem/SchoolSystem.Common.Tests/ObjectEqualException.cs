using Xunit.Sdk;

namespace SchoolSystem.Common.Tests;

public class ObjectEqualException : XunitException
{
	public ObjectEqualException(object? expected, object? actual, string message)
: base($"Expected: {expected}\nActual: {actual}\n{message}")
	{
		Message = message;
	}

	public override string Message { get; }
}