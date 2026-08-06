using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class Setting
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Key { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Value { get; set; } = string.Empty;
}