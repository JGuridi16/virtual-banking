using Microsoft.AspNetCore.SignalR;

namespace VirtualBanking.Web.Hubs
{
    public class SmartQueueHub : Hub
    {
        public async Task SendQueue(string message)
        {
            await Clients.All.SendAsync("ReceiveQueue", message);
        }
    }
}
