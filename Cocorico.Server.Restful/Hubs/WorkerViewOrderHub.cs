using Cocorico.Shared.Hubs;
using Cocorico.Shared.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cocorico.Server.Restful.Hubs
{
    [Authorize(Roles = Claims.Worker)]
    public class WorkerViewOrderHub : Hub<IWorkerViewOrderClient>
    {
    }
}
