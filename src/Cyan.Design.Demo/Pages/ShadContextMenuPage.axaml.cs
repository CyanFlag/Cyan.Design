using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class ShadContextMenuPage : UserControl
{
    public ShadContextMenuPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}