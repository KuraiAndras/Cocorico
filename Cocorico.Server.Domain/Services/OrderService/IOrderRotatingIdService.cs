using System;

namespace Cocorico.Server.Domain.Services.OrderService
{
    public interface IOrderRotatingIdService
    {
        void SetRange(Range range);
        int GetNextId();
    }
}
