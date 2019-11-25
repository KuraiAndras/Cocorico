using System;

namespace Cocorico.Server.Domain.Services.SystemNotifications
{
    public class SystemNotifier<T> : ISystemNotifier<T>
    {
        public void NotifyOrderAdded(T item) => OnNotification?.Invoke(item);

        public event Action<T>? OnNotification;
    }
}