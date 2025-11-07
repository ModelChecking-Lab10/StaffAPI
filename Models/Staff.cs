using System;
using System.Collections.Generic;

namespace StaffAPI.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime StartingDate { get; set; }

    public string? Photo { get; set; }
}
