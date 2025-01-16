using AutoMapper;
using Klinik.BL.DTOs;
using Klinik.BL.Services.Abstractions;
using Klinik.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Klinik.BL.Services.Concretes;

public class AccountService : IAccountService
{
    readonly UserManager<IdentityUser> _userManager;
    readonly SignInManager<IdentityUser> _signInManager;
    readonly IMapper _mapper;

    public AccountService(IMapper mapper, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task LoginAsync(UserLoginDTO dto)
    {
        IdentityUser user = await _userManager.FindByNameAsync(dto.UserName) ?? throw new Exception("Credentials are wrong!");

        SignInResult res = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!res.Succeeded) throw new Exception("Credentials are wrong!");
    }

    public async Task RegisterAsync(UserRegisterDTO dto)
    {
        IdentityUser user = _mapper.Map<IdentityUser>(dto);

        if (await _userManager.FindByNameAsync(user.UserName) is not null) throw new Exception("Something went wrong!");

        if (await _userManager.FindByEmailAsync(user.Email) is not null) throw new Exception("Something went wrong!");

        IdentityResult res = await _userManager.CreateAsync(user);

        if (!res.Succeeded) throw new Exception("Something went wrong!");

        res = await _userManager.AddToRoleAsync(user, Roles.User.ToString());

        if (!res.Succeeded) throw new Exception("Something went wrong!");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
