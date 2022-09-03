using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IUserService
{
    Task<ChatUser> register(CreateUserDto input);
    Task<ChatUser> Login(LoginDto input);
}