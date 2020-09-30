using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Objects
{
    public class ProcessResponse<T>
    {
        public ProcessResponse(T data = default)
        {
            Data = data;
        }

        public ProcessResponseType ResponseType { get; private set; }

        public string Message { get; private set; }

        public T Data { get; }

        public bool IsSuccess => ResponseType == ProcessResponseType.Success;

        public bool IsError => ResponseType == ProcessResponseType.Error;

        public ProcessResponse<T> SetState(ProcessResponseType status, string message)
        {
            ResponseType = status;
            Message = message;
            return this;
        }

        public ProcessResponse<T> Success(string message = "OK")
        {
            ResponseType = ProcessResponseType.Success;
            Message = message;
            return this;
        }

        public ProcessResponse<T> Error(string message = "Error")
        {
            ResponseType = ProcessResponseType.Error;
            Message = message;
            return this;
        }
    }
}