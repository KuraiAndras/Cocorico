namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommand : CommandDtoBase<int>
    {
        public DeleteOrderCommand(int dto)
            : base(dto)
        {
        }
    }
}
