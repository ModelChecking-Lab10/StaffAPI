using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StaffAPI.Models;

public partial class Staff
{
    [Key]
    public int StaffId { get; set; }

    [Required(ErrorMessage = "Staff name is required.")]
    public string StaffName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[^\s@]+@[^\s@.]+\.[^\s@.]+(\.[^\s@.]+)*$", ErrorMessage = "Invalid email format.")] // <-- THÊM
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^(?:(?:\(?\+\d{1,3}\)?(?:[ \-]?\(?\d{1,4}\)?){1,3})|(?:0(?:(?:\(\d{2,3}\))|\d{2,3})(?:[ \-]?\d{3})(?:[ \-]?\d{3,4})?))$", ErrorMessage = "Invalid phone number format (10-15 digits, optional +).")] // <-- THÊM
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Starting date is required.")]
    public DateTime StartingDate { get; set; }

    public string? Photo { get; set; }
}