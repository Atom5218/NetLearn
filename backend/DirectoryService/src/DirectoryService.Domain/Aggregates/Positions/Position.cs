using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Positions;

public sealed class Position
{
    public PositionId Id { get; private set; }

    public PositionName Name { get; private set; } 

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private Position(
        PositionName name, 
        string? description)
    { 
        Id = PositionId.New();
        Name = name;
        IsActive = true;

        Description = description;

        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public static Result<Position, ValidationError> Create(string name, string? description)
    {
        var nameResult = PositionName.Create(name);
        if (nameResult.IsFailure)
            return nameResult.Error;


        return new Position(nameResult.Value, description);
    }
}
