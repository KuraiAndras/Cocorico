using MediatR;

namespace Cocorico.Application
{
    public abstract class CommandDtoBase<TDto> : DtoMessageBase<TDto>, IRequest
    {
        protected CommandDtoBase(TDto dto)
            : base(dto)
        {
        }
    }
}
