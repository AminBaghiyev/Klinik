using AutoMapper;
using Klinik.BL.DTOs;
using Klinik.BL.Exceptions;
using Klinik.BL.Services.Abstractions;
using Klinik.Core.Models;
using Klinik.DL.Repository.Abstractions;

namespace Klinik.BL.Services.Concretes;

public class DepartmentService : IDepartmentService
{
    readonly IRepository<Department> _repository;
    readonly IMapper _mapper;

    public DepartmentService(IRepository<Department> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<DepartmentListItemDTO>> GetAllListItemsAsync(int count = 0) => _mapper.Map<ICollection<DepartmentListItemDTO>>(await _repository.GetAllAsync(count: count, orderAsc: false));

    public async Task<ICollection<DepartmentViewItemDTO>> GetAllViewItemsAsync(int count = 3) => _mapper.Map<ICollection<DepartmentViewItemDTO>>(await _repository.GetAllAsync(count: count, orderAsc: false));

    public async Task<DepartmentUpdateDTO> GetByIdForUpdateAsync(int id) => _mapper.Map<DepartmentUpdateDTO>(await GetByIdAsync(id));

    public async Task<Department> GetByIdAsync(int id) => await _repository.GetOneAsync(e => e.Id == id) ?? throw new BaseException("Department not found!");

    public async Task<Department> GetByIdWithChildrenAsync(int id) => await _repository.GetOneAsync(e => e.Id == id, includes: "Doctors") ?? throw new BaseException("Department not found!");

    public async Task CreateAsync(DepartmentCreateDTO dto, string username)
    {
        Department department = _mapper.Map<Department>(dto);
        department.CreatedBy = username;
        department.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _repository.CreateAsync(department);
    }

    public async Task UpdateAsync(DepartmentUpdateDTO dto, string username)
    {
        Department oldDepartment = await GetByIdAsync(dto.Id);
        Department department = _mapper.Map<Department>(dto);
        department.CreatedBy = oldDepartment.CreatedBy;
        department.CreatedAt = oldDepartment.CreatedAt;
        department.UpdatedBy = username;
        department.UpdatedAt = DateTime.UtcNow.AddHours(4);

        _repository.Update(department);
    }

    public async Task DeleteAsync(int id)
    {
        Department department = await GetByIdWithChildrenAsync(id);

        if (department.Doctors.Count > 0) throw new BaseException("This department has doctors!");

        _repository.Delete(department);
    }

    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}
