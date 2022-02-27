using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MUXC = Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Windows.UI.Text;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ChatApp.UniversalWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HubConnection connection;

        public MainPage()
        {
            this.InitializeComponent();
            sendButton.IsEnabled = false;


        }
        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new HubConnectionBuilder()
                     .WithUrl(new Uri("http://localhost/hostchatnode/chathub"))
                     .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                     .Build();

                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    AppendMessage($"{user} : {message}");

                });

                await this.connection.StartAsync();
                await this.connection.SendAsync("SendMessage", "Desktop Client", "Connection started");
                AppendMessage("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
        private void AppendMessage(string message)
        {
            MUXC.InfoBadge infoBadge = new MUXC.InfoBadge();
            infoBadge.Style = (Style)App.Current.Resources["SuccessIconInfoBadgeStyle"];
            infoBadge.Width = 32;
            infoBadge.Height = 32;
            infoBadge.FontSize = 32;
            infoBadge.FontWeight = FontWeights.Bold;
            messagesList.Items?.Add(infoBadge);
            messagesList.Items?.Add(message);
        }
        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.connection.SendAsync("SendMessage", this.userTextBox.Text.ToString(), this.messageTextBox.Text.ToString());
                AppendMessage($"{this.userTextBox.Text.ToString()} : { this.messageTextBox.Text.ToString() }");
                this.userTextBox.IsEnabled = false;
                this.messageTextBox.Text = "";
            }
            catch (Exception ex)
            {
                AppendMessage($"Error sending: {ex}");
            }
        }
    }
}
