using Avalonia;
using System;
using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Helpers;

namespace TextileWarehouseERP;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // Initialize database before starting the UI
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={DbPathHelper.GetDatabasePath()}");

        using (var context = new AppDbContext(optionsBuilder.Options))
        {
            DbInitializer.Initialize(context);
        }

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}