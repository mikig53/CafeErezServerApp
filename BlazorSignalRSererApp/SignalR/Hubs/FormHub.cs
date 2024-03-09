using BetModels.Interfaces;
using BetModels.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSignalRSererApp.SignalR.Hubs
{
    public class FormHub : Hub
    {
        private readonly Dictionary<string, string> _telephoneNumberToConnectionId = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            // Add the connection ID of the current connection to a group that includes only the server itself
            await Groups.AddToGroupAsync(Context.ConnectionId, "Server");
        }

        public async Task SendForm(Form form)
        {
           
            // Send the form to the group that includes only the server itself
            await Clients.Group("Server").SendAsync("ReceiveForm", form);
        }

       
    }
}
