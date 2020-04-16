using System;

namespace Cocorico.Application.Orders.Services.RotatingId
{
    public class MemoryOrderRotatingIdService : IOrderRotatingIdService
    {
        private static readonly object IdAndRangeLock = new object();
        private static Range _idRange = new Range(1, 5);
        private static int _id = _idRange.Start.Value;

        public void SetRange(Range range)
        {
            lock (IdAndRangeLock)
            {
                if (_idRange.Start.Value == range.Start.Value && _idRange.End.Value == range.End.Value) return;

                _idRange = range;
            }
        }

        public Range GetRange()
        {
            lock (IdAndRangeLock)
            {
                return _idRange;
            }
        }

        public int GetNextId()
        {
            lock (IdAndRangeLock)
            {
                var idToReturn = _id;

                _id++;

                if (_id >= _idRange.End.Value) _id = _idRange.Start.Value;

                return idToReturn;
            }
        }
    }
}
