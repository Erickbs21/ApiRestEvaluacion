namespace MiBancoAPI.Models.DTOs;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string Message = "",
    List<string>? Errors = null
)
{
    public static ApiResponse<T> SuccessResult(T data, string message = "Operación exitosa") =>
        new(true, data, message);

    public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null) =>
        new(false, default, message, errors);
}
