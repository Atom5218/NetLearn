using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Departments;

/// <summary>
/// Полное имя
/// </summary>
public sealed record DepartmentName
{
    public const int MAX_LENGTH = 100;

    public string Value { get; }

    private DepartmentName(string value) => Value = value;


    public static Result<DepartmentName, ValidationError> Create(string? value)
    {

        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(nameof(value));

        // нормализовать значение
        string trimmed = value.Trim();

        if (trimmed.Length > MAX_LENGTH)
            return Errors.General.ValueIsTooLong(nameof(value), MAX_LENGTH);

        return new DepartmentName(trimmed);
    }
}
