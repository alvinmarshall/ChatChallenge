using Microsoft.AspNetCore.SignalR;

namespace App.Hubs;

public class ChatHub : Hub
{
    public const string ON_MESSAGE_RECEIVED = "RECIEVED_FROM_CHAT_ROOM";
}