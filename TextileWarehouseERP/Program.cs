using Avalonia;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Helpers;
using TextileWarehouseERP.Services;
using TextileWarehouseERP.ViewModels;

namespace TextileWarehouseERP;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={DbPathHelper.GetDatabasePath()}");

        using (var context = new AppDbContext(optionsBuilder.Options))
        {
            DbInitializer.Initialize(context);
        }

        var services = new ServiceCollection();
        services.AddSingleton(optionsBuilder.Options);
        services.AddSingleton<AppDbContext>(sp => new AppDbContext(optionsBuilder.Options));
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IAuditService, AuditService>();
        services.AddSingleton<IItemService, ItemService>();
        services.AddSingleton<MainViewModel>();

        App.Services = services.BuildServiceProvider();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}