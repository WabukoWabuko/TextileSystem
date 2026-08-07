using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextileWarehouseERP.Models;
using TextileWarehouseERP.Services;

namespace TextileWarehouseERP.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IItemService _itemService;
    private readonly ISettingsService _settingsService;

    public ObservableCollection<Item> Items { get; } = new();

    public List<string> Categories { get; } = new()
    {
        "Fabric", "Yarn", "Thread", "Accessory", "Finished Garment", "Packaging", "Other"
    };

    public List<string> FabricTypes { get; } = new()
    {
        "Cotton", "Polyester", "Cotton-Poly Blend", "Linen", "Silk", "Wool", "Denim", "Knitted", "Woven", "Other"
    };

    public List<string> Colors { get; } = new()
    {
        "White", "Black", "Red", "Blue", "Green", "Yellow", "Navy", "Grey", "Beige", "Multicolor", "Other"
    };

    public List<string> Units { get; } = new()
    {
        "Meter", "Yard", "Kg", "Piece", "Roll", "Cone", "Dozen", "Box"
    };

    public List<string> Sections { get; } = new()
    {
        "A", "B", "C", "D", "E", "Quarantine", "Returns"
    };

    public List<string> StatusOptions { get; } = new()
    {
        "Active", "Inactive"
    };

    [ObservableProperty]
    private string companyName = "Your Textile Company Ltd";

    [ObservableProperty]
    private string warehouseName = "Main Textile Warehouse";

    [ObservableProperty]
    private string statusMessage = "Ready to manage inventory.";

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private Item? selectedItem;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isEditing;

    [ObservableProperty]
    private string itemCode = string.Empty;

    [ObservableProperty]
    private string itemName = string.Empty;

    [ObservableProperty]
    private string category = "Fabric";

    [ObservableProperty]
    private string fabricType = "Cotton";

    [ObservableProperty]
    private string color = "White";

    [ObservableProperty]
    private string size = string.Empty;

    [ObservableProperty]
    private string unitOfMeasure = "Meter";

    [ObservableProperty]
    private string warehouseSection = "A";

    [ObservableProperty]
    private string rackLocation = string.Empty;

    [ObservableProperty]
    private string supplier = string.Empty;

    [ObservableProperty]
    private decimal reorderLevel;

    [ObservableProperty]
    private decimal currentStock;

    [ObservableProperty]
    private string status = "Active";

    [ObservableProperty]
    private int itemRating = 4;

    public string RatingDisplay => new string('★', ItemRating) + new string('☆', 5 - ItemRating);

    partial void OnItemRatingChanged(int value)
    {
        OnPropertyChanged(nameof(RatingDisplay));
    }

    public MainViewModel(IItemService itemService, ISettingsService settingsService)
    {
        _itemService = itemService;
        _settingsService = settingsService;
    }

    [RelayCommand]
    public async Task LoadItemsAsync()
    {
        try
        {
            IsBusy = true;
            StatusMessage = "Loading inventory...";
            var items = await _itemService.GetAllAsync(SearchText);
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }

            StatusMessage = items.Any()
                ? $"Loaded {items.Count} inventory items."
                : "No items found. Create a new product to get started.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        await LoadItemsAsync();
    }

    [RelayCommand]
    public void NewItem()
    {
        SelectedItem = null;
        IsEditing = false;
        ItemCode = string.Empty;
        ItemName = string.Empty;
        Category = Categories.First();
        FabricType = FabricTypes.First();
        Color = Colors.First();
        Size = string.Empty;
        UnitOfMeasure = Units.First();
        WarehouseSection = Sections.First();
        RackLocation = string.Empty;
        Supplier = string.Empty;
        ReorderLevel = 0;
        CurrentStock = 0;
        Status = "Active";
        ItemRating = 4;
        StatusMessage = "Ready to create a fresh item.";
    }

    [RelayCommand]
    public void EditItem()
    {
        if (SelectedItem == null)
        {
            StatusMessage = "Select an item in the inventory list to edit.";
            return;
        }

        IsEditing = true;
        ItemCode = SelectedItem.ItemCode;
        ItemName = SelectedItem.ItemName;
        Category = SelectedItem.Category;
        FabricType = SelectedItem.FabricType;
        Color = SelectedItem.Color;
        Size = SelectedItem.Size;
        UnitOfMeasure = SelectedItem.UnitOfMeasure;
        WarehouseSection = SelectedItem.WarehouseSection;
        RackLocation = SelectedItem.RackLocation;
        Supplier = SelectedItem.Supplier;
        ReorderLevel = SelectedItem.ReorderLevel;
        CurrentStock = SelectedItem.CurrentStock;
        Status = SelectedItem.Status;
        ItemRating = 4;
        StatusMessage = $"Editing {SelectedItem.ItemCode}.";
    }

    [RelayCommand]
    public async Task SaveItemAsync()
    {
        if (string.IsNullOrWhiteSpace(ItemName))
        {
            StatusMessage = "Item name is required.";
            return;
        }

        try
        {
            IsBusy = true;
            if (IsEditing && SelectedItem != null)
            {
                SelectedItem.ItemName = ItemName;
                SelectedItem.Category = Category;
                SelectedItem.FabricType = FabricType;
                SelectedItem.Color = Color;
                SelectedItem.Size = Size;
                SelectedItem.UnitOfMeasure = UnitOfMeasure;
                SelectedItem.WarehouseSection = WarehouseSection;
                SelectedItem.RackLocation = RackLocation;
                SelectedItem.Supplier = Supplier;
                SelectedItem.ReorderLevel = ReorderLevel;
                SelectedItem.CurrentStock = CurrentStock;
                SelectedItem.Status = Status;

                await _itemService.UpdateAsync(SelectedItem);
                StatusMessage = $"{SelectedItem.ItemCode} updated successfully.";
            }
            else
            {
                var item = new Item
                {
                    ItemCode = ItemCode,
                    ItemName = ItemName,
                    Category = Category,
                    FabricType = FabricType,
                    Color = Color,
                    Size = Size,
                    UnitOfMeasure = UnitOfMeasure,
                    WarehouseSection = WarehouseSection,
                    RackLocation = RackLocation,
                    Supplier = Supplier,
                    ReorderLevel = ReorderLevel,
                    CurrentStock = CurrentStock,
                    Status = Status
                };

                var created = await _itemService.CreateAsync(item);
                ItemCode = created.ItemCode;
                StatusMessage = $"Item {created.ItemCode} created.";
            }

            await LoadItemsAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task DeactivateItemAsync()
    {
        if (SelectedItem == null)
        {
            StatusMessage = "Select an item to deactivate.";
            return;
        }

        try
        {
            IsBusy = true;
            await _itemService.DeactivateAsync(SelectedItem.ItemCode);
            StatusMessage = $"{SelectedItem.ItemCode} has been marked inactive.";
            await LoadItemsAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }
}
