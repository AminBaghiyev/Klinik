using FluentValidation;

namespace Klinik.BL.DTOs;

public record DepartmentUpdateDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public class DepartmentUpdateDTOValidator : AbstractValidator<DepartmentUpdateDTO>
{
    public DepartmentUpdateDTOValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be bigger than zero!");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty!")
            .MaximumLength(100).WithMessage("The title can be up to 100 characters long!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty!")
            .MaximumLength(255).WithMessage("The description can be up to 255 characters long!");
    }
}