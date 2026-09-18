using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Cyan.Design.Core.Controls.Overlays;

namespace Cyan.Design.Demo.Pages;

public partial class ShadCommandPage : UserControl
{
    private CyanShadCommandDialog? _dialogWithOverlay;
    private CyanShadCommandDialog? _dialogNoOverlay;
    private TextBlock? _resultText;

    public ShadCommandPage()
    {
        InitializeComponent();
        _dialogWithOverlay = this.FindControl<CyanShadCommandDialog>("DialogWithOverlay");
        _dialogNoOverlay = this.FindControl<CyanShadCommandDialog>("DialogNoOverlay");
        _resultText = this.FindControl<TextBlock>("ResultText");
        AddHandler(KeyDownEvent, OnGlobalKeyDown, Avalonia.Interactivity.RoutingStrategies.Tunnel);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnOpenWithOverlayClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_dialogWithOverlay is not null)
            _dialogWithOverlay.IsOpen = true;
    }

    private void OnOpenWithoutOverlayClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_dialogNoOverlay is not null)
            _dialogNoOverlay.IsOpen = true;
    }

    private void OnGlobalKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.K && e.KeyModifiers == KeyModifiers.Control)
        {
            if (_dialogWithOverlay is not null)
                _dialogWithOverlay.IsOpen = !_dialogWithOverlay.IsOpen;
            e.Handled = true;
        }
    }

    private void OnItemClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            if (_resultText is not null)
                _resultText.Text = $"Selected: {btn.Content}";
            if (_dialogWithOverlay is not null)
                _dialogWithOverlay.IsOpen = false;
            if (_dialogNoOverlay is not null)
                _dialogNoOverlay.IsOpen = false;
        }
    }
}
