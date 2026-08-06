using System;

using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class InboundTransaction
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string TransactionNo { get; set; } = string.Empty;

    public DateTime DateReceived { get; set; } = DateTime.Today;

    [MaxLength(100)]
    public string Supplier { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    [MaxLength(150)]
    public string ItemName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string WarehouseSection { get; set; } = string.Empty;

    public decimal QuantityReceived { get; set; }

    [MaxLength(20)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [MaxLength(100)]
    public string ReceivedBy { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Remarks { get; set; }

    [MaxLength(300)]
    public string? ImagePath { get; set; }

    [MaxLength(50)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(100)]
    public string? ComputerName { get; set; }
}