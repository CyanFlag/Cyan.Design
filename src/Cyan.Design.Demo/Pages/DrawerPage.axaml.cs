using Avalonia.Controls;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Feedback;
using DrawerPlacement = Cyan.Design.Core.Controls.Feedback.DrawerPlacement;

namespace Cyan.Design.Demo.Pages;

public partial class DrawerPage : UserControl
{
    public DrawerPage()
    {
        InitializeComponent();
    }

    private void OnOpenBasic(object? sender, RoutedEventArgs e) => BasicDrawer.Open = true;

    private void OnOpenTop(object? sender, RoutedEventArgs e)
    {
        PlacementDrawer.Placement = DrawerPlacement.Top;
        PlacementDrawer.Open = true;
    }

    private void OnOpenRight(object? sender, RoutedEventArgs e)
    {
        PlacementDrawer.Placement = DrawerPlacement.Right;
        PlacementDrawer.Open = true;
    }

    private void OnOpenBottom(object? sender, RoutedEventArgs e)
    {
        PlacementDrawer.Placement = DrawerPlacement.Bottom;
        PlacementDrawer.Open = true;
    }

    private void OnOpenLeft(object? sender, RoutedEventArgs e)
    {
        PlacementDrawer.Placement = DrawerPlacement.Left;
        PlacementDrawer.Open = true;
    }

    private void OnOpenDefaultSize(object? sender, RoutedEventArgs e)
    {
        SizeDrawer.Size = DrawerSize.Default;
        SizeDrawer.CustomSize = null;
        SizeDrawer.Open = true;
    }

    private void OnOpenLargeSize(object? sender, RoutedEventArgs e)
    {
        SizeDrawer.Size = DrawerSize.Large;
        SizeDrawer.CustomSize = null;
        SizeDrawer.Open = true;
    }

    private void OnOpenCustomSize(object? sender, RoutedEventArgs e)
    {
        SizeDrawer.CustomSize = 500;
        SizeDrawer.Open = true;
    }

    private void OnOpenNoMask(object? sender, RoutedEventArgs e) => NoMaskDrawer.Open = true;
    private void OnOpenWithFooter(object? sender, RoutedEventArgs e) => FooterDrawer.Open = true;
    private void OnOpenWithExtra(object? sender, RoutedEventArgs e) => ExtraDrawer.Open = true;

    private void OnCloseFooter(object? sender, RoutedEventArgs e) => FooterDrawer.Open = false;
    private void OnCloseExtra(object? sender, RoutedEventArgs e) => ExtraDrawer.Open = false;
}
