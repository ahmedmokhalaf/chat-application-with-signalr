 # Cross-platform Real-time Chat Application.
  
 **Blazor , Desktop and Mobile Clients** Use same server hosting for chatting and host make scale out by _Redis_ to handled by the same server process.

 ## What is project's here?
 
> .NET class library [Host](https://github.com/ahmedmokhalaf/chat-application-with-signalr/tree/main/ChatApp.Host)

> .NET class library [Server](https://github.com/ahmedmokhalaf/chat-application-with-signalr/tree/main/ChatApp.Hubs).

> Blazor [WEB Client](https://github.com/ahmedmokhalaf/chat-application-with-signalr/tree/main/ChatApp.BlazorWeb).

> Universal Windows Platform (UWP) [Desktop Client](https://github.com/ahmedmokhalaf/chat-application-with-signalr/tree/main/ChatApp.UniversalWindows).

> Xamarin.Forms [Mobile Client](https://github.com/ahmedmokhalaf/chat-application-with-signalr/tree/main/ChatApp.XamarinForms).

## How to install and run the project.
- clone
- deploy host applications to server 
- change host url 
- download redis 
- **Edit ChatApp.Host\stratup class  to your redis configuration**

  ```csharp
  services.AddSignalR().AddStackExchangeRedis("localhost:6379", configure =>
              {
                  configure.Configuration.ChannelPrefix = "signalrchatapp";
              });
  ```
- deploy or debug blazor web or desktop app or mobile app or all of them
- Enjoy 

## Software requirements 
 - IIS local or server
 - [Redis](https://redis.io/).

## Screen When All Applications are Working Together
![This is an image](https://github.com/ahmedmokhalaf/chat-application-with-signalr/blob/main/ChatApp.gif?raw=true)

