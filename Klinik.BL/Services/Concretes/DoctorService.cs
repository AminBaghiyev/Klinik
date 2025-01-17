using AutoMapper;
using Klinik.BL.DTOs;
using Klinik.BL.Exceptions;
using Klinik.BL.Services.Abstractions;
using Klinik.BL.Utilities;
using Klinik.Core.Models;
using Klinik.DL.Repository.Abstractions;

namespace Klinik.BL.Services.Concretes;

public class DoctorService : IDoctorService
{
    readonly IRepository<Doctor> _repository;
    readonly IRepository<Department> _departmentRepository;
    readonly IMapper _mapper;

    public DoctorService(IRepository<Doctor> repository, IMapper mapper, IRepository<Department> departmentRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public async Task<ICollection<DoctorListItemDTO>> GetAllListItemsAsync(int count = 0) => _mapper.Map<ICollection<DoctorListItemDTO>>(await _repository.GetAllAsync(count: count, orderAsc: false, includes: "Department"));

    public async Task<ICollection<DoctorViewItemDTO>> GetAllViewItemsAsync(int count = 3) => _mapper.Map<ICollection<DoctorViewItemDTO>>(await _repository.GetAllAsync(count: count, orderAsc: false, includes: "Department"));

    public async Task<DoctorUpdateDTO> GetByIdForUpdateAsync(int id) => _mapper.Map<DoctorUpdateDTO>(await GetByIdAsync(id));

    public async Task<Doctor> GetByIdAsync(int id) => await _repository.GetOneAsync(e => e.Id == id) ?? throw new BaseException("Doctor not found!");

    public async Task<Doctor> GetByIdWithChildrenAsync(int id) => await _repository.GetOneAsync(e => e.Id == id, includes: "Department") ?? throw new BaseException("Doctor not found!");

    public async Task CreateAsync(DoctorCreateDTO dto, string username)
    {
        if (await _departmentRepository.GetOneAsync(e => e.Id == dto.DepartmentId) is null) throw new BaseException("Department not found!");

        Doctor doctor = _mapper.Map<Doctor>(dto);
        doctor.CreatedBy = username;
        doctor.CreatedAt = DateTime.UtcNow.AddHours(4);
        doctor.ThumbnailPath = await dto.Thumbnail.SaveAsync("doctors");

        await _repository.CreateAsync(doctor);
    }

    public async Task UpdateAsync(DoctorUpdateDTO dto, string username)
    {
        if (await _departmentRepository.GetOneAsync(e => e.Id == dto.DepartmentId) is null) throw new BaseException("Department not found!");

        Doctor oldDoctor = await GetByIdAsync(dto.Id);
        Doctor doctor = _mapper.Map<Doctor>(dto);
        doctor.CreatedBy = oldDoctor.CreatedBy;
        doctor.CreatedAt = oldDoctor.CreatedAt;
        doctor.UpdatedBy = username;
        doctor.UpdatedAt = DateTime.UtcNow.AddHours(4);
        doctor.ThumbnailPath = dto.Thumbnail is not null ? await dto.Thumbnail.SaveAsync("doctors") : oldDoctor.ThumbnailPath;

        _repository.Update(doctor);

        if (dto.Thumbnail is null) File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "doctors", oldDoctor.ThumbnailPath));
    }

    public async Task DeleteAsync(int id)
    {
        Doctor doctor = await GetByIdWithChildrenAsync(id);
        
        _repository.Delete(doctor);

        File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "doctors", doctor.ThumbnailPath));
    }

    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}
