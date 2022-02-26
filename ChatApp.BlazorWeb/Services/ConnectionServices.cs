using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatApp.BlazorWeb.Services
{
    public class ConnectionServices
    {
        public static HubConnection connection { get; private set; }
        public ConnectionServices()
        {
            connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:53353/ChatHub")
               .Build();
        }

        public static async Task<string> ConnectStatus()
        {
            string connectStatus;
            try
            {
                await connection.StartAsync();
                connectStatus =  "Connection started";
            }
            catch (Exception ex)
            {
                connectStatus = ex.Message;
            }
            return connectStatus;
        }
    }
}
