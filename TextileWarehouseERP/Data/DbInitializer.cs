using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Create database and tables if they do not exist
        context.Database.EnsureCreated();

        // Seed only if empty
        if (context.Users.Any())
            return;

        // ---------- Default Users ----------
        context.Users.AddRange(
            new User
            {
                Username = "admin",
                PasswordHash = HashPassword("Admin@123"),
                Role = "Administrator",
                FullName = "System Administrator",
                IsActive = true,
                CreatedBy = "SYSTEM"
            },
            new User
            {
                Username = "warehouse",
                PasswordHash = HashPassword("Wh@12345"),
                Role = "Warehouse Officer",
                FullName = "Warehouse Officer",
                IsActive = true,
                CreatedBy = "SYSTEM"
            },
            new User
            {
                Username = "viewer",
                PasswordHash = HashPassword("View@123"),
                Role = "Viewer",
                FullName = "Read Only User",
                IsActive = true,
                CreatedBy = "SYSTEM"
            }
        );

        // ---------- Settings ----------
        context.Settings.AddRange(
            new Setting { Key = "CompanyName", Value = "Your Textile Company Ltd" },
            new Setting { Key = "WarehouseName", Value = "Main Textile Warehouse" },
            new Setting { Key = "NextItemCode", Value = "10001" },
            new Setting { Key = "ItemCodePrefix", Value = "ITM-" },
            new Setting { Key = "NextInboundNo", Value = "1" },
            new Setting { Key = "InboundPrefix", Value = "INB-" },
            new Setting { Key = "NextOutboundNo", Value = "1" },
            new Setting { Key = "OutboundPrefix", Value = "OUT-" },
            new Setting { Key = "LowStockDefault", Value = "50" }
        );

        // ---------- Lookup Values ----------
        var lookups = new List<LookupValue>();

        string[] categories = { "Fabric", "Yarn", "Thread", "Accessory", "Finished Garment", "Packaging", "Other" };
        for (int i = 0; i < categories.Length; i++)
            lookups.Add(new LookupValue { Category = "Category", Value = categories[i], SortOrder = i });

        string[] fabricTypes = { "Cotton", "Polyester", "Cotton-Poly Blend", "Linen", "Silk", "Wool", "Denim", "Knitted", "Woven", "Other" };
        for (int i = 0; i < fabricTypes.Length; i++)
            lookups.Add(new LookupValue { Category = "FabricType", Value = fabricTypes[i], SortOrder = i });

        string[] colors = { "White", "Black", "Red", "Blue", "Green", "Yellow", "Navy", "Grey", "Beige", "Multicolor", "Other" };
        for (int i = 0; i < colors.Length; i++)
            lookups.Add(new LookupValue { Category = "Color", Value = colors[i], SortOrder = i });

        string[] uoms = { "Meter", "Yard", "Kg", "Piece", "Roll", "Cone", "Dozen", "Box" };
        for (int i = 0; i < uoms.Length; i++)
            lookups.Add(new LookupValue { Category = "UOM", Value = uoms[i], SortOrder = i });

        string[] sections = { "A", "B", "C", "D", "E", "Quarantine", "Returns" };
        for (int i = 0; i < sections.Length; i++)
            lookups.Add(new LookupValue { Category = "Section", Value = sections[i], SortOrder = i });

        string[] departments = { "Production", "Cutting", "Sewing", "Finishing", "Quality", "Sales", "Admin", "Other" };
        for (int i = 0; i < departments.Length; i++)
            lookups.Add(new LookupValue { Category = "Department", Value = departments[i], SortOrder = i });

        context.LookupValues.AddRange(lookups);

        context.SaveChanges();
    }

    public static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}