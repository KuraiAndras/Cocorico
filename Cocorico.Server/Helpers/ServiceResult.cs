using Cocorico.Server.Exceptions;

namespace Cocorico.Server.Helpers
{
    public interface IServiceResult
    {
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IServiceResult<T>
    {
    }

    public class Success : IServiceResult
    {
    }

    public sealed class Success<T> : Success, IServiceResult<T>
    {
        public T Data { get; }

        public Success(T data)
        {
            Data = data;
        }
    }

    public class Fail : IServiceResult
    {
        public CocoricoException Exception { get; }

        public Fail(CocoricoException exception)
        {
            Exception = exception;
        }
    }

    public sealed class Fail<T> : Fail, IServiceResult<T>
    {
        public Fail(CocoricoException exception) : base(exception)
        {
        }
    }
}
