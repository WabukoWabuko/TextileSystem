using System.ComponentModel.DataAnnotations;

namespace TextileWarehouseERP.Models;

public class LookupValue
{
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string Category { get; set; } = string.Empty; // e.g. "Category", "Color", "UOM", "Section"

    [Required, MaxLength(50)]
    public string Value { get; set; } = string.Empty;

    public int SortOrder { get; set; }
}