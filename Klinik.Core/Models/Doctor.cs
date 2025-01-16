using Klinik.Core.Models.Base;

namespace Klinik.Core.Models;

public class Doctor : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? FacebookURL { get; set; }
    public string? InstagramURL { get; set; }
    public string? TwitterURL { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
}
