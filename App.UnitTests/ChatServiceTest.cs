using App.DTO;
using App.Services;
using Domain.Model;
using FluentAssertions;
using Moq;
using NServiceBus;

namespace App.UnitTests;

public class ChatServiceTest
{
    private readonly Mock<IChatRoomService> _chatRoomServiceMock = new();
    private readonly Mock<IMessageService> _messageServiceMock = new();
    private readonly Mock<IMessageSession> _messageSessionMock = new();
    private ChatService? _sut;

    [Fact]
    public async void ShouldPerformBotActionWithStockBotCommand()
    {
        var roomSecret = "secret";
        var roomId = Guid.Parse("4397830B-E229-4FB5-8376-92278D360F08");
        var roomName = "Test-Room";
        var userId = Guid.Parse("9788A3CD-752A-4D69-A77F-494F5C917A6A");

        _chatRoomServiceMock.Setup(service => service.GetRoomBySecret(roomSecret))
            .ReturnsAsync(() => new ChatRoom { Id = roomId, Name = roomName, Secret = roomSecret });


        _sut = new ChatService(_chatRoomServiceMock.Object, _messageServiceMock.Object, _messageSessionMock.Object);
        var messageDto = new ChatRoomMessageDto()
        {
            Message = "/stock=AAPL.US",
            Secret = roomSecret,
            UserId = userId
        };
        var hubDto = await _sut.ParseMessage(messageDto);
        hubDto.Message.Should().Be("...");
        hubDto.Sender.Should().Be("bot");
    }

    [Fact]
    public async void ShouldGetStockCodeRequiredMessageFromBotIfStockCommandIsNull()
    {
        var roomSecret = "secret";
        var roomId = Guid.Parse("4397830B-E229-4FB5-8376-92278D360F08");
        var roomName = "Test-Room";
        var userId = Guid.Parse("9788A3CD-752A-4D69-A77F-494F5C917A6A");

        _chatRoomServiceMock.Setup(service => service.GetRoomBySecret(roomSecret))
            .ReturnsAsync(() => new ChatRoom { Id = roomId, Name = roomName, Secret = roomSecret });


        _sut = new ChatService(_chatRoomServiceMock.Object, _messageServiceMock.Object, _messageSessionMock.Object);
        var messageDto = new ChatRoomMessageDto()
        {
            Message = "/stock",
            Secret = roomSecret,
            UserId = userId
        };
        var hubDto = await _sut.ParseMessage(messageDto);
        hubDto.Message.Should().Be("stock code required!");
        hubDto.Sender.Should().Be("bot");
    }

    [Fact]
    public async void ShouldNotPerformBotActionOnNormalMessages()
    {
        var roomSecret = "secret";
        var roomId = Guid.Parse("4397830B-E229-4FB5-8376-92278D360F08");
        var roomName = "Test-Room";
        var userId = Guid.Parse("9788A3CD-752A-4D69-A77F-494F5C917A6A");
        var userName = "Test-User-Name";

        _chatRoomServiceMock.Setup(service => service.GetRoomBySecret(roomSecret))
            .ReturnsAsync(() => new ChatRoom { Id = roomId, Name = roomName, Secret = roomSecret });

        _chatRoomServiceMock.Setup(service => service.SaveMessage(It.IsAny<ChatRoomMessageDto>()))
            .ReturnsAsync(() => new ChatMessage()
            {
                message = "Test-Message", Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Room = new ChatRoom
                {
                    Id = roomId, Name = "Test-Room", Secret = roomSecret
                },
                User = new ChatUser { Id = userId, Name = userName }
            });

        _sut = new ChatService(_chatRoomServiceMock.Object, _messageServiceMock.Object, _messageSessionMock.Object);
        var messageDto = new ChatRoomMessageDto()
        {
            Message = "Test-Message",
            Secret = roomSecret,
            UserId = userId
        };
        var hubDto = await _sut.ParseMessage(messageDto);
        hubDto.Message.Should().Be("Test-Message");
        hubDto.Sender.Should().Be(userName);
    }
}