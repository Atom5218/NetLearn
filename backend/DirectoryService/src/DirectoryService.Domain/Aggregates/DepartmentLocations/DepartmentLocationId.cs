using DirectoryService.Domain.Aggregates.Departments;

namespace DirectoryService.Domain.Aggregates.DepartmentLocations;

public record class DepartmentLocationId
{
    public Guid Value { get; }

    private DepartmentLocationId(Guid value) => Value = value;

    public static DepartmentLocationId New() => new(Guid.NewGuid());
}