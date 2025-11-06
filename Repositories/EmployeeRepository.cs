using Microsoft.EntityFrameworkCore;
using StaffAPI.Data;

public class EmployeeRepository : IEmployeeRepository
{
  private readonly StaffsContext context;

  public EmployeeRepository(StaffsContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<Employee>> GetEmployees()
  {
    return await context.Employees.Include(e => e.Department).Include(e => e.Gender).ToListAsync();
  }

  public async Task<Employee> GetEmployee(int id)
  {
    return await context.Employees.Include(e => e.Department).Include(e => e.Gender).FirstOrDefaultAsync(e => e.EmployeeId == id);
    // return await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
  }

  public async Task<Employee> AddEmployee(Employee employee)
{
    var result = await context.Employees.AddAsync(employee);
    await context.SaveChangesAsync();
    return result.Entity;
}

  public async Task<Employee> UpdateEmployee(Employee employee)
  {
    var result = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

    if (result != null)
    {
      result.EmployeeId = employee.EmployeeId;
      result.FirstName = employee.FirstName;
      result.LastName = employee.LastName;
      result.Email = employee.Email;
      result.DateofBirth = employee.DateofBirth;
      result.GenderId = employee.GenderId;
      result.DepartmentId = employee.DepartmentId;
      await context.SaveChangesAsync();
      return result;
    }

    return null;
  }

  public async Task<Employee> DeleteEmployee(int id)
  {
    var result = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);

    if (result != null)
    {
      context.Employees.Remove(result);
      await context.SaveChangesAsync();
      return result;
    }

    return null;
  }
}