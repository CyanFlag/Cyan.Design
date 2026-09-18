using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Cyan.Design.Core.Controls.Icons;
using Cyan.Design.Core.Controls.Navigation;
using Cyan.Design.Core.Controls.Windowing;
using Cyan.Design.Demo.ViewModels;

namespace Cyan.Design.Demo;

public partial class MainWindow : CyanAppWindow
{
    private ThemePreset _currentTheme = ThemePreset.AntDesign;
    private readonly Dictionary<CyanNavigationViewItem, Func<object>> _pageFactories = [];

    public MainWindow()
    {
        InitializeComponent();
        BuildNavigation();
        NavView.SelectionChanged += OnSelectionChanged;
        if (NavView.Items.Count > 0)
            NavView.SelectedItem = NavView.Items[0];
    }

    private void BuildNavigation()
    {
        var vm = new NavigationViewModel();
        foreach (var group in vm.Groups)
        {
            foreach (var navItem in group.Items)
            {
                var icon = new CyanIcon { IconKey = navItem.IconKey, FontSize = 18 };
                var item = new CyanNavigationViewItem
                {
                    Header = navItem.Header,
                    Icon = icon
                };
                _pageFactories[item] = navItem.PageFactory;
                NavView.Items.Add(item);
            }
        }
    }

    private void OnSelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        if (NavView.SelectedItem is not { } selected) return;
        if (selected.Content is not null) return;
        if (_pageFactories.TryGetValue(selected, out var factory))
            selected.Content = factory();
    }

    private void OnLightClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => Application.Current!.RequestedThemeVariant = ThemeVariant.Light;

    private void OnDarkClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;

    private void OnAntDesignClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _currentTheme = ThemePreset.AntDesign;
        App.SwitchTheme(_currentTheme);
    }

    private void OnShadcnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _currentTheme = ThemePreset.Shadcn;
        App.SwitchTheme(_currentTheme);
    }

    private void OnParkUIClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _currentTheme = ThemePreset.ParkUI;
        App.SwitchTheme(_currentTheme);
    }

    private void OnMantineClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _currentTheme = ThemePreset.Mantine;
        App.SwitchTheme(_currentTheme);
    }
}
