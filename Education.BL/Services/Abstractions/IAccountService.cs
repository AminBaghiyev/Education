using Education.BL.DTOs;

namespace Education.BL.Services.Abstractions;

public interface IAccountService
{
    Task LoginAsync(UserLoginDTO dto);
    Task RegisterAsync(UserRegisterDTO dto);
    Task LogoutAsync();
}
