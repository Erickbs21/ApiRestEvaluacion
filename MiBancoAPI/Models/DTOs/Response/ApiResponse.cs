namespace MiBancoAPI.Models.DTOs.Response;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string Message = "",
    List<string>? Errors = null,
    object? Metadata = null
)
{
    public static ApiResponse<T> SuccessResult(T data, string message = "Operación exitosa", object? metadata = null) =>
        new(true, data, message, null, metadata);

    public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null) =>
        new(false, default, message, errors);

    public static ApiResponse<T> ValidationErrorResult(List<string> errors) =>
        new(false, default, "Errores de validación", errors);
}
