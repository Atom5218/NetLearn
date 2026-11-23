using CSharpFunctionalExtensions;
using DirectoryService.Domain.Aggregates.Departments;
using DirectoryService.Domain.Aggregates.Locations;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.DepartmentLocations;

public class DepartmentLocation
{
    private DepartmentLocation(DepartmentLocationId id, DepartmentId departmentId, LocationId locationId)
    {
        Id = id;
        DepartmentId = departmentId;
        LocationId = locationId;
    }

    public DepartmentLocationId Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; }

    public LocationId LocationId { get; private set; }

    public static Result<DepartmentLocation, ValidationError> Create(
        DepartmentId? departmentId,
        LocationId? locationId)
    {
        if (departmentId is null)
            return Errors.General.ValueIsRequired(nameof(departmentId));

        if (locationId is null)
            return Errors.General.ValueIsRequired(nameof(locationId));

        return new DepartmentLocation(
            DepartmentLocationId.New(),
            departmentId,
            locationId);
    }
}
