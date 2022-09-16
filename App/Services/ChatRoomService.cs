using App.DTO;
using App.Exceptions;
using Domain.Model;
using Domain.Repository;

namespace App.Services;

public class ChatRoomService : IChatRoomService
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatRoomService(
        IChatRoomRepository chatRoomRepository,
        IChatMessageRepository chatMessageRepository)
    {
        _chatRoomRepository = chatRoomRepository;
        _chatMessageRepository = chatMessageRepository;
    }


    public async Task<ChatMessage> SaveMessage(ChatRoomMessageDto input)
    {
        var room = await GetRoomById(input.RoomId);
        if (room is null) throw new RecordNotFoundException("Chat Secret Not Found");
        var chatMessage = new ChatMessage
        {
            message = input.Message,
            CreatedAt = DateTime.UtcNow,
            User = new ChatUser { Id = input.UserId },
            Room = room
        };
        if (input.IsBot) return chatMessage;
        return await _chatMessageRepository.SaveMessageAsync(chatMessage);
    }

    public async Task<ChatRoom> AddRoom(CreateRoomDto input)
    {
        var room = new ChatRoom
        {
            Name = input.Name,
            Secret = input.Secret,
            Users = input.Users
        };
        return await _chatRoomRepository.Add(room);
    }

    public async Task<ChatRoom> JoinRoom(ChatRoom input)
    {
        return await _chatRoomRepository.Update(input);
    }

    public async Task<ChatRoom> GetRoomById(Guid Id)
    {
        var chatRoom = await _chatRoomRepository.GetRoom(Id);
        if (chatRoom is null) throw new RecordNotFoundException("Chat Secret Not Found");
        return chatRoom;
    }

    public Task<List<ChatRoom>> Rooms()
    {
        return _chatRoomRepository.GetRooms();
    }
}