using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class ShadToastPage : UserControl
{
    private CyanShadToaster? _toaster;

    public ShadToastPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _toaster = this.FindControl<CyanShadToaster>("PART_Toaster");
    }

    private void OnDefaultClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Event created", "Sunday, December 3 at 9:00 AM");

    private void OnSuccessClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Success!", "Your changes have been saved.", ShadToastType.Success);

    private void OnInfoClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Information", "A new update is available.", ShadToastType.Info);

    private void OnWarningClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Warning", "Your subscription expires in 3 days.", ShadToastType.Warning);

    private void OnErrorClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Error", "Failed to save changes. Please try again.", ShadToastType.Error);

    private void OnLoadingClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => _toaster?.Show("Loading...", "Please wait while we process your request.", ShadToastType.Loading);
}