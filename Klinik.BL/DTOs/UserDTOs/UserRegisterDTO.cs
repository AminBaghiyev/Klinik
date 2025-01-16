using System.ComponentModel.DataAnnotations;

namespace Klinik.BL.DTOs;

public record UserRegisterDTO
{
    [Display(Prompt = "Username")]
    public string UserName { get; set; }

    [Display(Prompt = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Prompt = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Prompt = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
