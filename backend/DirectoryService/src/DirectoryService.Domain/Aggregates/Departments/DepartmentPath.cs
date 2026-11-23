using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Aggregates.Departments;

/// <summary>
/// Путь
/// </summary>
public record DepartmentPath
{
    public const int MAX_LENGTH = 100;
    public const string Separator = ".";
    private static readonly char SeparatorChar = Separator[0];
    public string Value { get;}
    private DepartmentPath(string value) => Value = value;
    public static DepartmentPath Root() => new(Separator);

    public static Result<DepartmentPath, ValidationError> CreateChild(DepartmentId id, DepartmentPath? parent)
    { 
        if (id is null)
            return Errors.General.ValueIsRequired(nameof(id));

        var prefix = parent?.Value ?? Separator;
        return new DepartmentPath($"{prefix}{id.Value}{Separator}");

    }

    public static Result<DepartmentPath, ValidationError> FromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(nameof(value));

        if (!value.StartsWith(Separator, System.StringComparison.Ordinal) || !value.EndsWith(Separator, System.StringComparison.Ordinal))
            return Errors.General.ValueIsRequired(nameof(value));

        var parts = value.Split(SeparatorChar, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
            if (!Guid.TryParse(part, out _))
                return Errors.General.ValueIsRequired(nameof(value));

        return new DepartmentPath(value);
    }
    public bool IsRoot => string.Equals(Value, Separator, StringComparison.Ordinal);

    public int Depth => Value.Split(SeparatorChar, StringSplitOptions.RemoveEmptyEntries).Length;

    public bool IsDescendant(DepartmentPath ancestor) => string.Equals(Value, ancestor.Value, StringComparison.Ordinal) && Value.StartsWith(ancestor.Value, StringComparison.Ordinal);
}
