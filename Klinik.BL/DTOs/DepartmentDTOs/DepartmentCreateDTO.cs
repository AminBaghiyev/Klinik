using FluentValidation;

namespace Klinik.BL.DTOs;

public record DepartmentCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class DepartmentCreateDTOValidator : AbstractValidator<DepartmentCreateDTO>
{
    public DepartmentCreateDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty!")
            .MaximumLength(100).WithMessage("The title can be up to 100 characters long!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty!")
            .MaximumLength(255).WithMessage("The description can be up to 255 characters long!");
    }
}