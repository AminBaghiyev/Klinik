using Klinik.BL.DTOs;

namespace Klinik.PL.ViewModels;

public class HomeVM
{
    public ICollection<DoctorViewItemDTO> Doctors { get; set; }
}
