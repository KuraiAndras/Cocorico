using MediatR;

namespace Cocorico.Application
{
    public abstract class EventDtoBase<TDto> : DtoMessageBase<TDto>, INotification
    {
        protected EventDtoBase(TDto dto) : base(dto)
        {
        }
    }
}
