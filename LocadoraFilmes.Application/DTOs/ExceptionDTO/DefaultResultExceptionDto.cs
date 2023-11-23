using System.Text.Json;

namespace LocadoraFilmes.Application.DTOs.ExceptionDTO
{
    public class DefaultResultExceptionDto
    {
        public string Message { get; private set; }
        public int StatusCode { get; private set; }
        public DefaultResultExceptionDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
