using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class ParkSpinnerPage : UserControl
{
    public ParkSpinnerPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}