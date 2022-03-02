using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ChatApp.XamarinForms
{
    public partial class MainPage : ContentPage
    {
        HubConnection connection;
        string UserName;
        ObservableCollection<string> ListMessage = new ObservableCollection<string>();
        public MainPage()
        {
            InitializeComponent();
            messagesList.ItemsSource = ListMessage;
            layoutChat.IsVisible = false;
            sendButton.IsVisible = true;
        }
        private async void GetUserName(object sender, EventArgs e)
        {
            UserName = await DisplayPromptAsync("HEllo...", "What's your name?");
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                connectButton_Clicked(sender, e);
                lblStatus.Text = "Welcome " + UserName;
            }
            else
            {
                await DisplayPromptAsync("Please Enter Your Name", "What's your name?");
            }
        }
        private async void connectButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                Button connectButton = new Button();
                connectButton.Text = "Connect";
                connection = new HubConnectionBuilder()
                     .WithUrl(new Uri("http://192.168.1.4/hostchatnode/chathub"))
                     .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                     .Build();

                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    AppendMessage($"{user} : {message}");

                });

                await this.connection.StartAsync();
                await this.connection.SendAsync("SendMessage", "Mobile Client", "Connection started");
                lblStatus.Text = "Welcome :" + this.UserName;
                AppendMessage("Connection started");
                btnConnectionPopup.IsVisible = false;
                layoutChat.IsVisible = true;
            }
            catch (Exception ex)
            {
                lblStatus.TextColor = Color.Red;
                lblStatus.Text = "Error : " + ex.Message;
            }
        }
        private void AppendMessage(string message)
        {
            ListMessage.Add(message);
            ChangeVisualState();
        }
        private async void sendButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await this.connection.SendAsync("SendMessage", this.UserName, this.messageTextBox.Text.ToString());
                AppendMessage($"{this.UserName} : { this.messageTextBox.Text.ToString() }");
                this.messageTextBox.Text = "";
            }
            catch (Exception ex)
            {
                AppendMessage($"Error sending: {ex}");
            }
        }

    }
}
