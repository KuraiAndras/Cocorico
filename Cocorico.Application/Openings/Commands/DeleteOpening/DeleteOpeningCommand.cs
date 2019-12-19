namespace Cocorico.Application.Openings.Commands.DeleteOpening
{
    // TODO: explicit dto class
    public sealed class DeleteOpeningCommand : CommandDtoBase<int>
    {
        public DeleteOpeningCommand(int dto) : base(dto)
        {
        }
    }
}
