using Microsoft.EntityFrameworkCore;
using StaffAPI.Models;

namespace StaffAPI.Repositories;

public class StaffRepository : IStaffRepository
{
  private readonly StaffsContext context;

  public StaffRepository(StaffsContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<Staff>> GetStaffs()
  {
    return await context.Staffs.ToListAsync();
  }

  public async Task<Staff> GetStaff(int id)
  {
    return await context.Staffs.FirstOrDefaultAsync(e => e.StaffId == id);
  }

  public async Task<Staff> AddStaff(Staff staff)
  {
    var result = await context.Staffs.AddAsync(staff);
    await context.SaveChangesAsync();
    return result.Entity;
  }

  public async Task<Staff> UpdateStaff(Staff staff)
  {
    var result = await context.Staffs.FirstOrDefaultAsync(e => e.StaffId == staff.StaffId);

    if (result != null)
    {
      result.StaffName = staff.StaffName;
      result.Email = staff.Email;
      result.PhoneNumber = staff.PhoneNumber;
      result.StartingDate = staff.StartingDate;
      result.Photo = staff.Photo;
      await context.SaveChangesAsync();
      return result;
    }

    return null;
  }

  public async Task<Staff> DeleteStaff(int id)
  {
    var result = await context.Staffs.FirstOrDefaultAsync(e => e.StaffId == id);

    if (result != null)
    {
      context.Staffs.Remove(result);
      await context.SaveChangesAsync();
      return result;
    }

    return null;
  }
}