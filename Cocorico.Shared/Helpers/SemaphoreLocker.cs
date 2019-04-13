using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Shared.Helpers
{
    public class SemaphoreLocker
    {
        private const int ThreadCount = 1;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(ThreadCount, ThreadCount);

        public async Task LockAsync(Func<Task> worker)
        {
            await _semaphore.WaitAsync();
            try
            {
                await worker();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
