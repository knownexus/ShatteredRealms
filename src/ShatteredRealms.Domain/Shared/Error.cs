using System.Net;

namespace ShatteredRealms.Domain.Shared;

public sealed class Error : IEquatable<Error>
{
    public Error(string title, string message, int code)
    {
        Code = code;
        Title = title;
        Message = message;
    }

    public static readonly Error None = new(string.Empty, string.Empty, (int)HttpStatusCode.OK);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", (int)HttpStatusCode.InternalServerError);

    public int Code { get; }
    public string Title { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Title;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Title == other.Title && Message == other.Message && Code == other.Code;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Title, Message, Code);

    public override string ToString() => Title;
}
