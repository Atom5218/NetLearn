using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Positions;

public sealed record PositionName
{
    public const int MinLength = 3;
    public const int MaxLength = 120;

    private PositionName(string name) => Name = name;

    public string Name { get; }

    public static Result<PositionName, ValidationError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired(value);
        }

        if (value.Length is < MinLength or > MaxLength)
        {
            return Errors.General.ValueIsInvalid(value);
        }

        return new PositionName(value);
    }
}
