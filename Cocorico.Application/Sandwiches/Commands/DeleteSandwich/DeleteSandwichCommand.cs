namespace Cocorico.Application.Sandwiches.Commands.DeleteSandwich
{
    public sealed class DeleteSandwichCommand : CommandDtoBase<int>
    {
        public DeleteSandwichCommand(int dto)
            : base(dto)
        {
        }
    }
}
