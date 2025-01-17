using Microsoft.AspNetCore.Http;

namespace Klinik.BL.DTOs;

public record DoctorCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IFormFile Thumbnail { get; set; }
    public string? FacebookURL { get; set; }
    public string? InstagramURL { get; set; }
    public string? TwitterURL { get; set; }
    public int DepartmentId { get; set; }
}
