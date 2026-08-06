using System;

using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class OutboundTransaction
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string TransactionNo { get; set; } = string.Empty;

    public DateTime DateIssued { get; set; } = DateTime.Today;

    [Required, MaxLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    [MaxLength(150)]
    public string ItemName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string WarehouseSection { get; set; } = string.Empty;

    [MaxLength(30)]
    public string RackLocation { get; set; } = string.Empty;

    public decimal QuantityIssued { get; set; }

    [MaxLength(20)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [MaxLength(100)]
    public string PickedBy { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Department { get; set; } = string.Empty;

    [MaxLength(50)]
    public string IssuedBy { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Remarks { get; set; }

    public decimal RemainingStock { get; set; }

    [MaxLength(50)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(100)]
    public string? ComputerName { get; set; }
}