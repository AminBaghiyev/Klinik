namespace Klinik.BL.DTOs;

public record DoctorListItemDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DepartmentTitle { get; set; }
}
