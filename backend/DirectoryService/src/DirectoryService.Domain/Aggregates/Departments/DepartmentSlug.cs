using CSharpFunctionalExtensions;
using DirectoryService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DirectoryService.Domain.Aggregates.Departments;

/// <summary>
/// Короткое имя
/// </summary>
public record DepartmentSlug
{
    public const int MAX_LENGTH = 150;
    public string Value { get; }
    private DepartmentSlug(string value) => Value = value;



    public static Result<DepartmentSlug, ValidationError> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(nameof(value));

        // нормализовать значение
        string normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > MAX_LENGTH)
            return Errors.General.ValueIsTooLong(nameof(value), MAX_LENGTH);

        if (!normalized.All(c => char.IsLetterOrDigit(c) || c == '-'))
            return Errors.General.ValueIsInvalid(nameof(value));

        return new DepartmentSlug(normalized);
    }

    public static Result<DepartmentSlug, ValidationError> FromName(string name)
    { 
        var slug = name.Trim().ToLowerInvariant().Replace(' ', '-');
        return Create(slug);
    }
}
