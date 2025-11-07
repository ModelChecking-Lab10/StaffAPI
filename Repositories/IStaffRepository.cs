using StaffAPI.Models;

namespace StaffAPI.Repositories;

public interface IStaffRepository
{
    Task<IEnumerable<Staff>> GetStaffs();
    Task<Staff> GetStaff(int id);
    Task<Staff> AddStaff(Staff staff);
    Task<Staff> UpdateStaff(Staff staff);
    Task<Staff> DeleteStaff(int id);
}