using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;

namespace Cyan.Design.Demo;

public enum ThemePreset
{
    AntDesign,
    Shadcn,
    ParkUI,
    Mantine
}

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static void SwitchTheme(ThemePreset theme)
    {
        if (Current is not App app) return;

        app.Resources.MergedDictionaries.Clear();
        var source = theme switch
        {
            ThemePreset.AntDesign => "avares://Cyan.Design.Themes/CyanDesignTheme.axaml",
            ThemePreset.Shadcn => "avares://Cyan.Design.Themes/CyanDesignShadcnTheme.axaml",
            ThemePreset.ParkUI => "avares://Cyan.Design.Themes/CyanDesignParkUITheme.axaml",
            ThemePreset.Mantine => "avares://Cyan.Design.Themes/CyanDesignMantineTheme.axaml",
            _ => "avares://Cyan.Design.Themes/CyanDesignTheme.axaml"
        };
        var include = (Avalonia.Controls.ResourceDictionary)AvaloniaXamlLoader.Load(new System.Uri(source));
        app.Resources.MergedDictionaries.Add(include);

        var variant = app.ActualThemeVariant;
        app.RequestedThemeVariant = null;
        app.RequestedThemeVariant = variant;
    }
}
