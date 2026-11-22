namespace DirectoryService.Domain.Common;

public static class Errors
{
    public static class General
    {
        public static ValidationError ValueIsRequired(string? field = null)
            => ValidationError.Create(
                code: "value.is.required",
                message: "Value is required.",
                invalidField: field);

        public static ValidationError ValueIsInvalid(string? field = null)
            => ValidationError.Create(
                code: "value.is.invalid",
                message: "Value is invalid.",
                invalidField: field);

        public static ValidationError ValueIsTooLong(string? field, int maxLength)
            => ValidationError.Create(
                code: "value.is.too.long",
                message: $"Value must not exceed {maxLength} characters.",
                invalidField: field);

        public static ValidationError NotFound(string? field = null)
            => ValidationError.Create(
                code: "value.not.found",
                message: "Value was not found.",
                invalidField: field);

        public static ValidationError ValueIsTooShort(string? field, int minLength)
            => ValidationError.Create(
                code: "value.is.too.short",
                message: $"Value must be at least {minLength} characters.",
                invalidField: field);
    }
}