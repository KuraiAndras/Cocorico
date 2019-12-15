using System;

namespace Cocorico.Application.Orders.Services.RotatingId
{
    public interface IOrderRotatingIdService
    {
        void SetRange(Range range);
        int GetNextId();
        Range GetRange();
    }
}
