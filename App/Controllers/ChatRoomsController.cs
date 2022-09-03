using App.DTO;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[Route("[controller]")]
[ApiController]
public class ChatRoomsController : Controller
{
    private readonly IChatService _chatService;

    public ChatRoomsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("message")]
    public async Task<ActionResult<ApiResponse<object>>> ParseMessage([FromBody] ChatRoomMessageDto input)
    {
        await _chatService.ParseMessage(input);
        var apiResponse = new ApiResponse<object> { Success = true };
        return Ok(apiResponse);
    }

    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<object>>> AddRoom([FromBody] CreateRoomDto input)
    {
        var apiResponse = new ApiResponse<object> { Success = true, Data = await _chatService.AddRoom(input) };
        return Ok(apiResponse);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> Rooms()
    {
        var apiResponse = new ApiResponse<object> { Success = true, Data = await _chatService.GetRooms() };
        return Ok(apiResponse);
    }
}