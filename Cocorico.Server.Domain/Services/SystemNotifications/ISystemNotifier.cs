using System;

namespace Cocorico.Server.Domain.Services.SystemNotifications
{
    public interface ISystemNotifier<T>
    {
        void NotifyOrderAdded(T item);
        event Action<T> OnNotification;
    }
}
