using Avalonia.Controls;

namespace Cyan.Design.Demo.Pages;

public partial class NavigationPage : UserControl
{
    public NavigationPage()
    {
        InitializeComponent();
        if (NavView.Items.Count > 0)
            NavView.SelectedItem = NavView.Items[0];
    }
}
