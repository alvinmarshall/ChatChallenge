using App.DTO;
using App.Exceptions;
using Domain.Model;
using Domain.Repository;

namespace App.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<ChatUser> register(CreateUserDto input)
    {
        var chatUser = new ChatUser
        {
            Name = input.Name
        };
        return _userRepository.CreateUser(chatUser);
    }

    public async Task<ChatUser> Login(LoginDto input)
    {
        var chatUser = await _userRepository.GetByName(input.Name);
        if (chatUser is null) throw new UserServiceException("UnAuthorize", ExceptionTypes.UnAuthorize);
        return chatUser;
    }
}