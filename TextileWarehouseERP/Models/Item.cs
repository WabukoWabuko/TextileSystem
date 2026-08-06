using System;

using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class Item
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string ItemName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FabricType { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Color { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Size { get; set; } = string.Empty;

    [MaxLength(20)]
    public string UnitOfMeasure { get; set; } = "Meter";

    [MaxLength(20)]
    public string WarehouseSection { get; set; } = string.Empty;

    [MaxLength(30)]
    public string RackLocation { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Supplier { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? ImagePath { get; set; }

    public decimal ReorderLevel { get; set; }

    public decimal CurrentStock { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "Active"; // Active / Inactive

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(50)]
    public string CreatedBy { get; set; } = string.Empty;
}