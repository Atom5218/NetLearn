using CSharpFunctionalExtensions;
using DirectoryService.Domain.Aggregates.Positions;
using DirectoryService.Domain.Common;
using System.Xml.Linq;

namespace DirectoryService.Domain.Aggregates.Locations;

public record LocationId
{
    public Guid Value { get; }

private LocationId(Guid value) => Value = value;

public static LocationId New() => new(Guid.NewGuid());
}