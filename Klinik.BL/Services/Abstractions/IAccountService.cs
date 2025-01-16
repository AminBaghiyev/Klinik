using Klinik.BL.DTOs;

namespace Klinik.BL.Services.Abstractions;

public interface IAccountService
{
    Task RegisterAsync(UserRegisterDTO dto);
    Task LoginAsync(UserLoginDTO dto);
    Task LogoutAsync();
}
