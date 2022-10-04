using System.Globalization;

namespace camsacreditoauto.Domain.Comun.Exceptions;

public class ApiException : Exception
{
    public ApiException() : base() { }

    public ApiException(string message) : base(message) { }

    public ApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {

    }
}
