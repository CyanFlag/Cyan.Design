using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Cyan.Design.Demo.Pages;

public partial class SpinPage : UserControl
{
    public SpinPage()
    {
        InitializeComponent();
        ToggleSpin.IsCheckedChanged += (_, _) => NestedSpin.Spinning = ToggleSpin.IsChecked == true;
    }
}
