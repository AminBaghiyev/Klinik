using Klinik.BL.DTOs;
using Klinik.Core.Models;

namespace Klinik.BL.Services.Abstractions;

public interface IDoctorService
{
    Task<ICollection<DoctorListItemDTO>> GetAllListItemsAsync(int count = 0);
    Task<ICollection<DoctorViewItemDTO>> GetAllViewItemsAsync(int count = 3);
    Task<Doctor> GetByIdAsync(int id);
    Task<Doctor> GetByIdWithChildrenAsync(int id);
    Task<DoctorUpdateDTO> GetByIdForUpdateAsync(int id);
    Task CreateAsync(DoctorCreateDTO dto, string username);
    Task UpdateAsync(DoctorUpdateDTO dto, string username);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
