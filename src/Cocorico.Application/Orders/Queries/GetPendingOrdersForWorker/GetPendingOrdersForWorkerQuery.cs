using Cocorico.Shared.Dtos.Orders;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Orders.Queries.GetPendingOrdersForWorker
{
    public sealed class GetPendingOrdersForWorkerQuery : IRequest<ICollection<WorkerOrderViewDto>>
    {
    }
}
