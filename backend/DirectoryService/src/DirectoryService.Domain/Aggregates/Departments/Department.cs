using CSharpFunctionalExtensions;
using DirectoryService.Domain.Aggregates.DepartmentLocations;
using DirectoryService.Domain.Aggregates.DepartmentPositions;
using DirectoryService.Domain.Aggregates.Locations;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Departments;


public sealed class Department
{
    private readonly List<DepartmentLocation> _locations = [];
    private readonly List<DepartmentPosition> _positions = [];

    private readonly List<DepartmentId> _childrenDepartments = [];

    public DepartmentId Id { get; private set; }

    public DepartmentName Name { get; private set; }

    public DepartmentSlug Slug { get; private set; }

    public DepartmentPath Path { get; private set; }

    public DepartmentId? ParentId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<DepartmentLocation> Locations => _locations;
    public IReadOnlyList<DepartmentPosition> Positions => _positions;
    public IReadOnlyList<DepartmentId> ChildrenDepartments => _childrenDepartments;

    private Department(
        DepartmentId id,
        DepartmentName name, 
        DepartmentSlug slug, 
        DepartmentPath path, 
        DepartmentId? parentId,
        IEnumerable<DepartmentLocation> locations)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Path = path;
        ParentId = parentId;

        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;

        _locations.AddRange(locations);
    }


    public static Result<Department, ValidationError> CreateRoot(
        string name,
        IEnumerable<LocationId> locationIds)
    {
        return CreateInternal(
            name,
            locationIds,
            parentId: null,
            parentPath: null
            );
    }

    public Result<Department, ValidationError> CreateChild(
        string name,
        IEnumerable<LocationId> locationIds)
    {
        var childResult = CreateInternal(
            name,
            locationIds,
            parentId: Id,
            parentPath: Path
            );

        if (childResult.IsFailure)
            return childResult.Error;

        var child = childResult.Value;

        _childrenDepartments.Add(child.Id);
        UpdatedAt = DateTime.UtcNow;

        return child;
    }

    private static Result<Department, ValidationError> CreateInternal(
        string name,
        IEnumerable<LocationId> locationIds,
        DepartmentId? parentId = null,
        DepartmentPath? parentPath = null)
    {
        var nameResult = DepartmentName.Create(name);
        if (nameResult.IsFailure)
            return nameResult.Error;

        var slugResult = DepartmentSlug.FromName(name);
        if (slugResult.IsFailure)
            return slugResult.Error;

        var id = DepartmentId.New();

        var pathResult = DepartmentPath.CreateChild(id, parentPath);
        if (pathResult.IsFailure)
            return pathResult.Error;

        var locations = new List<DepartmentLocation>();

        foreach (var locationId in locationIds)
        {
            var locationResult = DepartmentLocation.Create(id, locationId);
            if (locationResult.IsFailure)
                return locationResult.Error;

            locations.Add(locationResult.Value);
        }

        return new Department(
            id,
            nameResult.Value,
            slugResult.Value,
            pathResult.Value,
            parentId,
            locations);
    }

}
