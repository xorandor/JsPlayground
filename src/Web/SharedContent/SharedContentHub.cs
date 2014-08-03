using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web.SharedContent
{
    [HubName("sharedContent")]
    public class SharedContentHub : Hub<ISharedContentClient>
    {
        public override Task OnConnected()
        {
            Clients.Caller.StatusUpdate("Welcome my friend!");
            Clients.AllExcept(Context.ConnectionId).StatusUpdate("Sssh someone entered!");
            return Task.FromResult(0);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Clients.All.StatusUpdate("Someone left us... ");
            return Task.FromResult(0);
        }

        public void SetGlobalMessage(string message)
        {
            Clients.All.GlobalMessageReceived(message);
        }
    }

    public interface ISharedContentClient
    {
        void GlobalMessageReceived(string message);
        void StatusUpdate(string message);
    }
}