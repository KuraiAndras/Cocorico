using Cocorico.Shared.Exceptions;

namespace Cocorico.Shared.Services.Helpers
{
    public interface IServiceResult
    {
    }

    public readonly struct Success : IServiceResult
    {
    }

    public readonly struct Fail : IServiceResult
    {
        public CocoricoException Exception { get; }

        public Fail(CocoricoException exception = null) => Exception = exception ?? new UnexpectedException();
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IServiceResult<T>
    {
    }

    public readonly struct Success<T> : IServiceResult<T>
    {
        public T Data { get; }

        public Success(T data) => Data = data;
    }

    public readonly struct Fail<T> : IServiceResult<T>
    {
        public CocoricoException Exception { get; }

        public Fail(CocoricoException exception = null) => Exception = exception ?? new UnexpectedException();
    }
}
