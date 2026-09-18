using Avalonia.Controls;
using Cyan.Design.Core;

namespace Cyan.Design.Demo.Pages;

public partial class ButtonPage : UserControl
{
    public ButtonPage()
    {
        InitializeComponent();
    }

    private async void OnLoadingClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Core.Controls.Buttons.CyanButton btn)
        {
            btn.Loading = true;
            await System.Threading.Tasks.Task.Delay(1500);
            btn.Loading = false;
        }
    }
}
