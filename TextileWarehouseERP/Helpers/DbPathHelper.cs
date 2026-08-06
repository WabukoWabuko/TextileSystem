using System;
using System.IO;

namespace TextileWarehouseERP.Helpers;

public static class DbPathHelper
{
    public static string GetDatabasePath()
    {
        // Database will be stored in a "Data" folder next to the executable
        var folder = Path.Combine(AppContext.BaseDirectory, "Data");
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, "warehouse.db");
    }
}