namespace Cocorico.Application.Openings.Commands.DeleteOpening
{
    public sealed class DeleteOpeningCommand : CommandDtoBase<int>
    {
        public DeleteOpeningCommand(int dto)
            : base(dto)
        {
        }
    }
}
