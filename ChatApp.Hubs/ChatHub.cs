using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string fromUser, string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", fromUser, message);
        }

        public async Task SendPrivateMessage(string fromUser, string message)
        {
            await Clients.User(fromUser).SendAsync("ReceiveMessage", fromUser, message);
        }

        public async Task SendGroupMessage(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("GroupMessage", Context.User.Identity.Name, message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("GroupInfoMessage", $"{Context.User.Identity.Name} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("GroupInfoMessage", $"{Context.User.Identity.Name} has left the group {groupName}.");
        }
    }
}
