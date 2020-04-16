namespace Cocorico.Application
{
    public abstract class DtoMessageBase<TDto>
    {
        protected DtoMessageBase(TDto dto) => Dto = dto;

        public TDto Dto { get; }
    }
}