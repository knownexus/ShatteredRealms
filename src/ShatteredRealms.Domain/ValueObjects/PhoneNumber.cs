using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Primatives;
using ShatteredRealms.Domain.Shared;
using System.Text.RegularExpressions;

namespace ShatteredRealms.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public static int MaxLength { get; } = 15; // international phone numbers
    public static int MinLength { get; } = 8;

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    private static readonly Regex PhoneRegex = new(@"^\+?\d{8,15}$");
    // Allows optional + at start, followed by 8-15 digits

    public static Result<PhoneNumber> Create(string phoneNumber) =>
        Result.Create(phoneNumber)
              .Ensure(
                      e => !string.IsNullOrWhiteSpace(e),
                      DomainErrors.PhoneNumber.Empty)
              .Ensure(
                      e => e.Length >= MinLength,
                      DomainErrors.PhoneNumber.TooShort)
              .Ensure(
                      e => e.Length <= MaxLength,
                      DomainErrors.PhoneNumber.TooLong)
              .Ensure(
                      e => PhoneRegex.IsMatch(e),
                      DomainErrors.PhoneNumber.InvalidFormat)
              .Map(e => new PhoneNumber(e));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}