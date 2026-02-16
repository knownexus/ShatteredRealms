using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Primatives;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    public static int MaxLength {get; } = 255;
    public static int RequiredLength {get; } = 8;
    public static bool RequireDigit {get; } = true;
    public static bool RequireLowercase {get; } = true;
    public static bool RequireUppercase {get; } = true;
    public static bool RequireNonAlphanumeric {get; } = true;

    private Password(string value)
    {
        Value = value;
    }

    private Password()
    {
    }

    public string Value { get; private set; }

    public static Result<Password> Create(string password) =>
        Result.Create(password)
              .Ensure(
                      e => !string.IsNullOrWhiteSpace(e),
                      DomainErrors.Password.Empty)
              .Ensure(
                      e => e.Length <= MaxLength,
                      DomainErrors.Password.TooLong)
              .Ensure(
                      e => e.Length > RequiredLength,
                      DomainErrors.Password.TooShort)
              .Ensure(
                      e => !RequireDigit || e.Any(char.IsDigit),
                      DomainErrors.Password.RequireDigit)
              .Ensure(
                      e => !RequireNonAlphanumeric || e.Any(c => !char.IsLetterOrDigit(c)),
                      DomainErrors.Password.RequireNonAlphanumeric)
              .Ensure(
                      e => !RequireLowercase || e.Any(char.IsLower),
                      DomainErrors.Password.RequireLowercase)
              .Ensure(
                      e => !RequireUppercase || e.Any(char.IsUpper),
                      DomainErrors.Password.RequireUppercase)
              .Map(e => new Password(e));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
