using System;

using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class AuditLog
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Details { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? ComputerName { get; set; }
}