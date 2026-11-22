using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Locations;

public record Address
{
    private const int MinLength = 2;
    private const int MaxLength = 100;
    private const int MaxApartmentLength = 20;
    public string Region { get; }
    public string City { get; }
    public string Street { get; }
    public string Building { get; }
    public string? Apartment { get; }

    private Address(
        string region, 
        string city, 
        string street, 
        string building, 
        string? apartment)
    { 
        Region = region;
        City = city;
        Street = street;
        Building = building;
        Apartment = apartment;
    }


    public static Result<Address, ValidationError> Create(
        string? region, 
        string? city, 
        string? street, 
        string? building, 
        string? apartment = null)
    {
        var regionResult = ValidateRequired(region, nameof(Region));
        if (regionResult.IsFailure)
            return regionResult.Error;

        var cityResult = ValidateRequired(city, nameof(City));
        if (cityResult.IsFailure)
            return cityResult.Error;

        var streetResult = ValidateRequired(street, nameof(Street));
        if (streetResult.IsFailure)
            return streetResult.Error;

        var buildingResult = ValidateRequired(building, nameof(Building));
        if (buildingResult.IsFailure)
            return buildingResult.Error;

        var apartmentResult = ValidateApartment(apartment);
        if (apartmentResult.IsFailure)
            return apartmentResult.Error;

        return new Address(
            regionResult.Value,
            cityResult.Value,
            streetResult.Value,
            buildingResult.Value,
            apartmentResult.Value);
    }

    private static Result<string, ValidationError> ValidateRequired(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(field);

        var trimmed = value.Trim();

        if (trimmed.Length < MinLength)
            return Errors.General.ValueIsTooShort(field, MinLength);

        if (trimmed.Length > MaxLength)
            return Errors.General.ValueIsTooLong(field, MaxLength);

        return trimmed;
    }

    private static Result<string?, ValidationError> ValidateApartment(string? apartment)
    {
        if (string.IsNullOrWhiteSpace(apartment))
            return (string?)null;

        var trimmed = apartment.Trim();

        if (trimmed.Length > MaxApartmentLength)
            return Errors.General.ValueIsTooLong(nameof(Apartment), MaxApartmentLength);

        return trimmed;
    }
}
