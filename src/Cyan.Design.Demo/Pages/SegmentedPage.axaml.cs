using Avalonia.Controls;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Demo.Pages;

public partial class SegmentedPage : UserControl
{
    public SegmentedPage()
    {
        InitializeComponent();
        if (ControlledSegmented != null)
            ControlledSegmented.PropertyChanged += OnSegmentedPropertyChanged;
    }

    private void OnSegmentedPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == CyanSegmented.SelectedValueProperty && SelectedValueText != null)
            SelectedValueText.Text = $"当前选中: {ControlledSegmented?.SelectedValue}";
    }
}
