using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Locations;

public sealed class Location
{
    private readonly List<Address> _addresses = [];

    public LocationId Id { get; private set; }

    public LocationName Name { get; private set; }

    public LocationTimezone Timezone { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<Address> Addresses => _addresses;

    private Location(
        LocationName name, 
        LocationTimezone timezone, 
        Address address)
    {
        Id = LocationId.New();
        Name = name;
        Timezone = timezone;
        IsActive = true;
        _addresses.Add(address);

        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public static Result<Location, ValidationError> Create(
        string name,
        LocationTimezone timezone,
        Address address
        )
    {
        var nameResult = LocationName.Create(name);
        if (nameResult.IsFailure)
            return nameResult.Error;

        return new Location(nameResult.Value, timezone, address);

    }

    public UnitResult<ValidationError> AddAddress(Address address)
    {
        if (Addresses.Contains(address))
        {
            return Errors.General.ValueIsInvalid("location.address");
        }

        _addresses.Add(address);
        return UnitResult.Success<ValidationError>();
    }
}
