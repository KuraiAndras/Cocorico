using System;
using Cocorico.Shared.Helpers;

namespace Cocorico.Server.Helpers
{
    public class ServiceResult<T> : ServiceResult
    {
        private readonly T _data;

        public T Data => IsSuccessful ? _data : throw Exception;

        public ServiceResult(T data, string message = Messages.Ok)
            : base(message)
        {
            _data = data;
        }

        public ServiceResult(Exception exception)
            : base(exception)
        {
        }
    }

    public class ServiceResult
    {
        public bool IsSuccessful { get; }

        public Exception Exception { get; }

        public string Message { get; }

        public ServiceResult()
            : this(Messages.Ok)
        {
        }

        public ServiceResult(string message)
            : this(true, message)
        {
        }

        public ServiceResult(Exception exception)
            : this(false, string.Empty)
        {
            Exception = exception;
        }

        private ServiceResult(bool isSuccess, string message)
        {
            IsSuccessful = isSuccess;
            Message = message;
        }
    }
}
