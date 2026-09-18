using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class EditablePage : UserControl
{
    public EditablePage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}