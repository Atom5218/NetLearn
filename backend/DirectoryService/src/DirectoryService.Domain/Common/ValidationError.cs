using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DirectoryService.Domain.Common;

public record ValidationError
{
    public string Code { get; init; } = default!;
    public string Message { get; init; } = default!;
    public string? InvalidField { get; init; }

    private ValidationError() { }

    public static ValidationError Create(string code, string message, string? invalidField = null) => new() { Code = code, Message = message, InvalidField = invalidField };
}