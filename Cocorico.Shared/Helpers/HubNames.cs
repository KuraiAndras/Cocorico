namespace Cocorico.Shared.Helpers
{
    public static class HubNames
    {
        public static class WorkerViewOrderHubNames
        {
            public const string Name = "/WorkerViewOrderHub";
            public const string ReceiveOrdersAsync = nameof(ReceiveOrdersAsync);
        }
    }
}
