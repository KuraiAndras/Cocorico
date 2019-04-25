﻿using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Order
{
    public class ClientOrderService : IClientOrderService
    {
        private readonly HttpClient _httpClient;

        public ClientOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IServiceResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync(string customerId) =>
            await _httpClient.RetrieveDataFromServerAsync<IEnumerable<OrderCustomerViewDto>>(HttpVerbs.Get, Urls.Server.GetAllOrderForCustomer + $"/{customerId}");

        public async Task<IServiceResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync() =>
            await _httpClient.RetrieveDataFromServerAsync<IEnumerable<OrderWorkerViewDto>>(HttpVerbs.Get, Urls.Server.PendingOrdersForWorker);

        public async Task<IServiceResult> AddOrderAsync(OrderAddDto orderAddDto) =>
            await _httpClient.RetrieveMessageFromServerAsync(HttpVerbs.Post, Urls.Server.OrderBase, orderAddDto);

        public async Task<IServiceResult> DeleteOrderAsync(int orderId) =>
            await _httpClient.RetrieveMessageFromServerAsync(HttpVerbs.Delete, Urls.Server.OrderBase + $"/{orderId}", "");
    }
}
