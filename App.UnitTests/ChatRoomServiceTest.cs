using App.DTO;
using App.Exceptions;
using App.Services;
using Domain.Model;
using Domain.Repository;
using FluentAssertions;
using Moq;

namespace App.UnitTests;

public class ChatRoomServiceTest
{
    private readonly Mock<IChatRoomRepository> _chatRoomRepositoryMock = new();
    private readonly Mock<IChatMessageRepository> _chatMessageRepositoryMock = new();
    private ChatRoomService? _sut;

    [Fact]
    public async void ShouldSaveMessage()
    {
        var secret = "secret";
        var userId = Guid.Parse("9788A3CD-752A-4D69-A77F-494F5C917A6A");
        var roomId = Guid.Parse("4397830B-E229-4FB5-8376-92278D360F08");
        var message = "Test-Message";
        _chatRoomRepositoryMock.Setup(repository => repository.GetBySecreteAsync(secret))
            .ReturnsAsync(() => new ChatRoom { Id = roomId, Name = "Test-Room", Secret = secret });
        _chatMessageRepositoryMock.Setup(repository => repository.SaveMessageAsync(It.IsAny<ChatMessage>()))
            .ReturnsAsync(() => new ChatMessage
            {
                Id = Guid.NewGuid(),
                message = message,
                Room = new ChatRoom { Id = roomId, Name = "Test-Room", Secret = secret },
                User = new ChatUser { Id = userId, Name = "Test-User-Name" }
            });


        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);
        var messageDto = new ChatRoomMessageDto
        {
            Secret = secret,
            UserId = userId,
            Message = message
        };

        var chatMessage = await _sut.SaveMessage(messageDto);
        chatMessage.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async void ShouldNotSaveMessageAndThrowIfRoomSecretIsInvalid()
    {
        var secret = "unknown";
        var userId = Guid.Parse("9788A3CD-752A-4D69-A77F-494F5C917A6A");
        _chatRoomRepositoryMock.Setup(repository => repository.GetBySecreteAsync(secret))
            .ReturnsAsync(() => null);

        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);
        var messageDto = new ChatRoomMessageDto
        {
            Secret = secret,
            UserId = userId,
            Message = "Test-Message"
        };

        var task = () => _sut.SaveMessage(messageDto);
        await task.Should().ThrowAsync<RecordNotFoundException>();
    }

    [Fact]
    public async void ShouldAddRoom()
    {
        var secret = "secret";
        var roomName = "Test-Room";
        _chatRoomRepositoryMock.Setup(repository => repository.Add(It.IsAny<ChatRoom>()))
            .ReturnsAsync(() => new ChatRoom { Id = Guid.NewGuid(), Name = roomName, Secret = secret });

        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);

        var chatRoom = await _sut.AddRoom(new CreateRoomDto { Name = roomName, Secret = secret });
        chatRoom.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async void ShouldGetRoomBySecret()
    {
        var secret = "secret";
        var roomName = "Test-Room";
        var roomId = Guid.Parse("4397830B-E229-4FB5-8376-92278D360F08");
        _chatRoomRepositoryMock.Setup(repository => repository.GetBySecreteAsync(secret))
            .ReturnsAsync(() => new ChatRoom { Id = roomId, Name = roomName, Secret = secret });

        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);

        var chatRoom = await _sut.GetRoomBySecret(secret);
        chatRoom.Id.Should().NotBeEmpty();
        chatRoom.Secret.Should().Be(secret);
    }

    [Fact]
    public async void ShouldThrowIfChatRoomSecretIsInvalid()
    {
        var secret = "unknown";
        _chatRoomRepositoryMock.Setup(repository => repository.GetBySecreteAsync(secret))
            .ReturnsAsync(() => null);

        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);

        var task = () => _sut.GetRoomBySecret(secret);
        await task.Should().ThrowAsync<RecordNotFoundException>();
    }

    [Fact]
    public async void ShouldGetAllRooms()
    {
        var secret = "secret";
        var roomName = "Test-Room";
        _chatRoomRepositoryMock.Setup(repository => repository.GetRooms())
            .ReturnsAsync(() => new List<ChatRoom>(new[]
                { new ChatRoom { Id = Guid.NewGuid(), Name = roomName, Secret = secret } }));

        _sut = new ChatRoomService(_chatRoomRepositoryMock.Object, _chatMessageRepositoryMock.Object);

        var chatRooms = await _sut.Rooms();
        chatRooms.Should().NotBeEmpty();
    }
}