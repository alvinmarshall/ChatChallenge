using App.DTO;
using App.Exceptions;
using App.Services;
using Domain.Model;
using Domain.Repository;
using FluentAssertions;
using Moq;

namespace App.UnitTests;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private UserService? _sut;

    [Fact]
    public async void ShouldRegisterUser()
    {
        var userName = "test-user";
        _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<ChatUser>()))
            .ReturnsAsync(() => new ChatUser() { Id = Guid.NewGuid(), Name = userName });
        _sut = new UserService(_userRepositoryMock.Object);

        var chatUser = await _sut.register(new CreateUserDto() { Name = userName });
        chatUser.Id.Should().NotBeEmpty();
        chatUser.Name.Should().Be(userName);
    }

    [Fact]
    public async void ShouldLoginUserWithValidCredentials()
    {
        var userName = "test-user";
        _userRepositoryMock.Setup(repository => repository.GetByName(userName))
            .ReturnsAsync(() => new ChatUser { Id = Guid.NewGuid(), Name = userName });
        _sut = new UserService(_userRepositoryMock.Object);

        var chatUser = await _sut.Login(new LoginDto() { Name = userName });
        chatUser.Id.Should().NotBeEmpty();
        chatUser.Name.Should().Be(userName);
    }

    [Fact]
    public async void ShouldThrowIfInvalidUserCredentials()
    {
        _userRepositoryMock.Setup(repository => repository.GetByName(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        _sut = new UserService(_userRepositoryMock.Object);
        var act = () => _sut.Login(new LoginDto() { Name = "some" });
        await act.Should().ThrowAsync<UserServiceException>();
    }
}