using Microsoft.AspNetCore.SignalR;
using VirtualBanking.Web.Models;

namespace VirtualBanking.Web.Hubs
{
    public class SmartQueueHub : Hub
    {
        public async Task AddCustomer(BankCustomer customerInfo)
        {
            await Clients.All.SendAsync("QueueCustomer", customerInfo);
        }
    }
}
