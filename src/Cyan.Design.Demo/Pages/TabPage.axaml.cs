using Avalonia.Controls;
using Cyan.Design.Core.Controls.Navigation;

namespace Cyan.Design.Demo.Pages;

public partial class TabPage : UserControl
{
    public TabPage()
    {
        InitializeComponent();
        foreach (var item in ClosableTabs.Items)
        {
            if (item is CyanTabItem tabItem)
            {
                tabItem.Closed += (_, _) =>
                {
                    ClosableTabs.Items.Remove(tabItem);
                };
            }
        }
    }
}
