using MediatR;

namespace Cocorico.Application
{
    public abstract class QueryDtoBase<TDto, TResponse> : DtoMessageBase<TDto>, IRequest<TResponse>
    {
        protected QueryDtoBase(TDto dto) : base(dto)
        {
        }
    }
}