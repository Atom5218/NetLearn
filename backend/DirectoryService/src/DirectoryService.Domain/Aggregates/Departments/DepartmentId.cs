namespace DirectoryService.Domain.Aggregates.Departments;

public record DepartmentId
{
    public Guid Value { get; }

    private DepartmentId(Guid value) => Value = value;

    public static DepartmentId New() => new(Guid.NewGuid());
}