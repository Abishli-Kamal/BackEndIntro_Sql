using Ap204_Pronia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Ap204_Pronia.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendMessage(string name,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",name,message);
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                user.ConnectionId = Context.ConnectionId;
                await _userManager.UpdateAsync(user);
                await Clients.All.SendAsync("UserConnected", user.Id);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                user.ConnectionId = null;
                await _userManager.UpdateAsync(user);
                await Clients.All.SendAsync("UserDisConnected", user.Id);
            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
