

using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DirectoryService.Domain.Aggregates.Locations;

public record LocationTimezone
{
    public string Value { get; }

    private LocationTimezone(string value)
    {
        Value = value;
    }

    public static Result<LocationTimezone, ValidationError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(value);

        var isValid = TimeZoneInfo.TryFindSystemTimeZoneById(value, out _);

        if (!isValid)
            return Errors.General.ValueIsInvalid(value);

        return new LocationTimezone(value);
    }
}