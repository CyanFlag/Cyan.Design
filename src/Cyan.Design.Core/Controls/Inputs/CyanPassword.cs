using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>密码输入框控件，支持可见性切换</summary>
public class CyanPassword : CyanInput
{
    /// <summary>是否显示可见性切换按钮样式属性</summary>
    public static readonly StyledProperty<bool> VisibilityToggleProperty =
        AvaloniaProperty.Register<CyanPassword, bool>(nameof(VisibilityToggle), true);

    /// <summary>密码是否可见样式属性</summary>
    public static readonly StyledProperty<bool> IsPasswordVisibleProperty =
        AvaloniaProperty.Register<CyanPassword, bool>(nameof(IsPasswordVisible));

    static CyanPassword()
    {
        IsPasswordVisibleProperty.Changed.AddClassHandler<CyanPassword>(OnPasswordVisibleChanged);
    }

    /// <summary>是否显示可见性切换按钮</summary>
    public bool VisibilityToggle
    {
        get => GetValue(VisibilityToggleProperty);
        set => SetValue(VisibilityToggleProperty, value);
    }

    /// <summary>密码是否可见</summary>
    public bool IsPasswordVisible
    {
        get => GetValue(IsPasswordVisibleProperty);
        set => SetValue(IsPasswordVisibleProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Find<Button>("PART_ToggleButton") is { } toggleBtn)
            toggleBtn.Click += OnToggleButtonClick;
        ApplyPasswordMask();
        UpdateTogglePseudoClass();
    }

    private static void OnPasswordVisibleChanged(CyanPassword c, AvaloniaPropertyChangedEventArgs e)
    {
        c.ApplyPasswordMask();
        c.UpdateTogglePseudoClass();
    }

    private void OnToggleButtonClick(object? sender, RoutedEventArgs e)
        => IsPasswordVisible = !IsPasswordVisible;

    private void ApplyPasswordMask()
        => PasswordChar = IsPasswordVisible ? '\0' : '●';

    private void UpdateTogglePseudoClass()
        => PseudoClasses.Set(":pwvisible", IsPasswordVisible);
}
