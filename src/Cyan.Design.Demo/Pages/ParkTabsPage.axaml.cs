using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class ParkTabsPage : UserControl
{
    public ParkTabsPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}