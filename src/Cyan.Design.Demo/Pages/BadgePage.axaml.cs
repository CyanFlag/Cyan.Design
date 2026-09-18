using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class BadgePage : UserControl
{
    public BadgePage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
