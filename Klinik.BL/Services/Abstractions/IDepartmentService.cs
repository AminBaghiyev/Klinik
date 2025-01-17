using Klinik.BL.DTOs;
using Klinik.Core.Models;

namespace Klinik.BL.Services.Abstractions;

public interface IDepartmentService
{
    Task<ICollection<DepartmentListItemDTO>> GetAllListItemsAsync(int count = 0);
    Task<ICollection<DepartmentViewItemDTO>> GetAllViewItemsAsync(int count = 3);
    Task<Department> GetByIdAsync(int id);
    Task<Department> GetByIdWithChildrenAsync(int id);
    Task<DepartmentUpdateDTO> GetByIdForUpdateAsync(int id);
    Task CreateAsync(DepartmentCreateDTO dto, string username);
    Task UpdateAsync(DepartmentUpdateDTO dto, string username);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
