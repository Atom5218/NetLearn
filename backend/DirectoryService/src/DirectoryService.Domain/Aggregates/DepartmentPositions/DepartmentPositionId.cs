using DirectoryService.Domain.Aggregates.DepartmentLocations;

namespace DirectoryService.Domain.Aggregates.DepartmentPositions;

public record DepartmentPositionId
{
    public Guid Value { get; }

    private DepartmentPositionId(Guid value) => Value = value;

    public static DepartmentPositionId New() => new(Guid.NewGuid());
}
