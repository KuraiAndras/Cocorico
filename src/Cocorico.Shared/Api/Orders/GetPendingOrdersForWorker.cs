using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Orders
{
    public sealed class GetPendingOrdersForWorker : IRequest<ICollection<WorkerOrderViewDto>>
    {
    }
}
