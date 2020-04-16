namespace Cocorico.Shared.Helpers
{
    public static class HubNames
    {
        public static class WorkerViewOrderHubNames
        {
            public const string Name = "/WorkerViewOrderHub";
            public const string ReceiveOrderAddedAsync = nameof(ReceiveOrderAddedAsync);
            public const string ReceiveOrderModifiedAsync = nameof(ReceiveOrderModifiedAsync);
            public const string ReceiveOrderDeletedAsync = nameof(ReceiveOrderDeletedAsync);
        }
    }
}
