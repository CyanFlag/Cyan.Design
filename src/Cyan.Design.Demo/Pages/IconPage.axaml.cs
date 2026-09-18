using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Icons;

namespace Cyan.Design.Demo.Pages;

public partial class IconPage : UserControl
{
    public IconPage()
    {
        InitializeComponent();
        IconGrid.ItemsSource = CyanIcons.AllKeys;
    }

    private void OnSearch(object? sender, TextChangedEventArgs e)
    {
        var keyword = SearchBox.Text?.Trim() ?? string.Empty;
        IconGrid.ItemsSource = string.IsNullOrEmpty(keyword)
            ? CyanIcons.AllKeys
            : CyanIcons.AllKeys.Where(k => k.Contains(keyword)).ToList();
    }
}
