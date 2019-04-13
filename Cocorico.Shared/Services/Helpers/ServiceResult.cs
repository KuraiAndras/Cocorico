using Cocorico.Shared.Exceptions;

namespace Cocorico.Shared.Services.Helpers
{
    public interface IServiceResult
    {
    }

    public sealed class Success : IServiceResult
    {
    }

    public sealed class Fail : IServiceResult
    {
        public CocoricoException Exception { get; }

        public Fail(CocoricoException exception)
        {
            Exception = exception;
        }
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IServiceResult<T>
    {
    }

    public sealed class Success<T> : IServiceResult<T>
    {
        public T Data { get; }

        public Success(T data)
        {
            Data = data;
        }
    }

    public sealed class Fail<T> : IServiceResult<T>
    {
        public CocoricoException Exception { get; }

        public Fail(CocoricoException exception)
        {
            Exception = exception;
        }
    }
}
