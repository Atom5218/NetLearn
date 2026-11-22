using CSharpFunctionalExtensions;
using DirectoryService.Domain.Aggregates.Departments;
using DirectoryService.Domain.Aggregates.Positions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.DepartmentPositions;

public sealed class DepartmentPosition
{
    private DepartmentPosition(DepartmentPositionId id, DepartmentId departmentId, PositionId positionId)
    {
        Id = id;
        DepartmentId = departmentId;
        PositionId = positionId;
    }

    public DepartmentPositionId Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; }

    public PositionId PositionId { get; private set; }

    public static Result<DepartmentPosition, ValidationError> Create(
        DepartmentId? departmentId,
        PositionId? positionId)
    {
        if (departmentId is null)
            return Errors.General.ValueIsRequired(nameof(departmentId));

        if (positionId is null)
            return Errors.General.ValueIsRequired(nameof(positionId));

        return new DepartmentPosition(
            DepartmentPositionId.New(),
            departmentId,
            positionId);
    }
}
