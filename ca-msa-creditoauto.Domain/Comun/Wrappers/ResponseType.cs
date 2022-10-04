namespace camsacreditoauto.Domain.Comun.Wrappers;


public class ResponseType<T>
{
    public ResponseType()
    {

    }

    public ResponseType(string message)
    {
        Succeeded = false;
        Message = message;
    }

    public ResponseType(T data, string? message = null)
    {
        Succeeded = true;
        Message = message ?? string.Empty;
        Data = data;
    }

    public bool Succeeded { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string StatusCode { get; set; } = string.Empty;

    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    public T? Data { get; set; }

}
