using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Positions;

public record PositionId
{
    public Guid Value { get; }

    private PositionId(Guid value) => Value = value;

    public static PositionId New() => new(Guid.NewGuid());
}