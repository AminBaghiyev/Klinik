namespace Klinik.BL.DTOs;

public record DoctorViewItemDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ThumbnailPath { get; set; }
    public string? FacebookURL { get; set; }
    public string? InstagramURL { get; set; }
    public string? TwitterURL { get; set; }
    public string DepartmentTitle { get; set; }
    public string FullName => FirstName + " " + LastName;
}
