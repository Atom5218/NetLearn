using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Locations;

public record LocationName
{
    public const int MinLength = 3;
    public const int MaxLength = 120;

    public string Value { get; }

    private LocationName(string value)
    {
        Value = value;
    }

    public static Result<LocationName, ValidationError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired(value);
        }

        if (value.Length is < MinLength or > MaxLength)
        {
            return Errors.General.ValueIsInvalid(value);
        }

        return new LocationName(value);
    }
}