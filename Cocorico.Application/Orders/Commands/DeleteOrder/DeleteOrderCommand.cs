namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    // TODO: explicit dto class
    public sealed class DeleteOrderCommand : CommandDtoBase<int>
    {
        public DeleteOrderCommand(int dto) : base(dto)
        {
        }
    }
}
