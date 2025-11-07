using Microsoft.AspNetCore.Mvc;
using StaffAPI.Models;
using StaffAPI.Repositories;

namespace StaffAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IStaffRepository staffRepository;

    public StaffController(IStaffRepository staffRepository)
    {
        this.staffRepository = staffRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
    {
        try
        {
            var staffs = await staffRepository.GetStaffs();
            return Ok(staffs);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Staff>> GetStaff(int id)
    {
        try
        {
            var staff = await staffRepository.GetStaff(id);
            if (staff == null)
                return NotFound($"Staff with Id = {id} not found.");

            return Ok(staff);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
    {
        try
        {
            if (staff == null)
                return BadRequest("Invalid staff data.");

            var createdStaff = await staffRepository.AddStaff(staff);

            return CreatedAtAction(nameof(GetStaff),
                new { id = createdStaff.StaffId },
                createdStaff);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating staff record.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Staff>> UpdateStaff(int id, Staff staff)
    {
        try
        {
            if (id != staff.StaffId)
                return BadRequest("Staff ID mismatch.");

            var existingStaff = await staffRepository.GetStaff(id);
            if (existingStaff == null)
                return NotFound($"Staff with Id = {id} not found.");

            var updated = await staffRepository.UpdateStaff(staff);
            return Ok(updated);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating staff record.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteStaff(int id)
    {
        try
        {
            var staff = await staffRepository.GetStaff(id);
            if (staff == null)
                return NotFound($"Staff with Id = {id} not found.");

            await staffRepository.DeleteStaff(id);
            return Ok(staff);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting staff record.");
        }
    }
}
