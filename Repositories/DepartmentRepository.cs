using StaffAPI.Data;

public class DepartmentRepository : IDepartmentRepository
{
  private readonly StaffsContext context;

  public DepartmentRepository(StaffsContext context)
  {
    this.context = context;
  }

  public IEnumerable<Department> GetDepartments()
  {
    return context.Departments;
  }

  public Department GetDepartment(int id)
  {
    return context.Departments.FirstOrDefault(d => d.DepId == id);
  }
}