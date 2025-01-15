using AutoMapper;
using Education.BL.DTOs;
using Education.BL.Services.Abstractions;
using Education.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Education.BL.Services.Concretes;

public class AccountService : IAccountService
{
    readonly UserManager<IdentityUser> _userManager;
    readonly SignInManager<IdentityUser> _signInManager;
    readonly IMapper _mapper;

    public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task LoginAsync(UserLoginDTO dto)
    {
        IdentityUser user = await _userManager.FindByNameAsync(dto.UserName) ?? throw new Exception();

        SignInResult res = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!res.Succeeded) { throw new Exception("Credentials are wrong!"); }
    }

    public async Task RegisterAsync(UserRegisterDTO dto)
    {
        IdentityUser user = _mapper.Map<IdentityUser>(dto);

        if (await _userManager.FindByNameAsync(user.UserName) is not null) throw new Exception();

        if (await _userManager.FindByEmailAsync(user.Email) is not null) throw new Exception();

        IdentityResult res = await _userManager.CreateAsync(user, dto.Password);

        if (!res.Succeeded) throw new Exception();

        res = await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

        if (!res.Succeeded) throw new Exception();
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
