using System.ComponentModel.DataAnnotations;

namespace Education.BL.DTOs;

public class UserLoginDTO
{
    [Display(Prompt = "Username")]
    public string UserName { get; set; }

    [Display(Prompt = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
