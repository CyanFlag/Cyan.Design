using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Overlays;

/// <summary>右键上下文菜单控件，右键触发时展示菜单</summary>
public class CyanShadContextMenu : TemplatedControl
{
    /// <summary>触发元素样式属性</summary>
    public static readonly StyledProperty<object?> TriggerProperty =
        AvaloniaProperty.Register<CyanShadContextMenu, object?>(nameof(Trigger));

    /// <summary>菜单内容样式属性</summary>
    public static readonly StyledProperty<object?> MenuProperty =
        AvaloniaProperty.Register<CyanShadContextMenu, object?>(nameof(Menu));

    /// <summary>触发元素</summary>
    public object? Trigger
    {
        get => GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>菜单内容</summary>
    public object? Menu
    {
        get => GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    private Popup? _popup;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _popup = e.NameScope.Find<Popup>("PART_Popup");
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed && _popup is not null)
        {
            _popup.IsOpen = true;
            e.Handled = true;
        }
    }
}

/// <summary>上下文菜单项控件</summary>
public class CyanShadContextMenuItem : Button
{
    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanShadContextMenuItem, object?>(nameof(Icon));

    /// <summary>快捷键样式属性</summary>
    public static readonly StyledProperty<string?> ShortcutProperty =
        AvaloniaProperty.Register<CyanShadContextMenuItem, string?>(nameof(Shortcut));

    /// <summary>图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>快捷键文本</summary>
    public string? Shortcut
    {
        get => GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    /// <summary>初始化 CyanShadContextMenuItem 的新实例</summary>
    public CyanShadContextMenuItem()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Left;
    }
}


/// <summary>上下文菜单分隔符控件</summary>
public class CyanShadContextMenuSeparator : Border
{
    /// <summary>初始化 CyanShadContextMenuSeparator 的新实例</summary>
    public CyanShadContextMenuSeparator()
    {
        Height = 1;
        Background = this.TryFindResource("ColorBorderBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#E4E4E7"));
        Margin = new Thickness(0, 4);
    }
}

/// <summary>上下文菜单分组标签控件</summary>
public class CyanShadContextMenuLabel : TextBlock
{
    /// <summary>初始化 CyanShadContextMenuLabel 的新实例</summary>
    public CyanShadContextMenuLabel()
    {
        FontSize = 12;
        FontWeight = FontWeight.Medium;
        Foreground = this.TryFindResource("ColorTextSecondaryBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#71717A"));
        Margin = new Thickness(8, 4, 0, 2);
    }
}

/// <summary>上下文菜单分组容器控件</summary>
public class CyanShadContextMenuGroup : StackPanel
{
    /// <summary>初始化 CyanShadContextMenuGroup 的新实例</summary>
    public CyanShadContextMenuGroup()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
    }
}

/// <summary>上下文菜单复选项控件</summary>
public class CyanShadContextMenuCheckboxItem : ToggleButton
{
    /// <summary>初始化 CyanShadContextMenuCheckboxItem 的新实例</summary>
    public CyanShadContextMenuCheckboxItem()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Left;
    }
}

/// <summary>上下文菜单单选项控件</summary>
public class CyanShadContextMenuRadioItem : RadioButton
{
    /// <summary>初始化 CyanShadContextMenuRadioItem 的新实例</summary>
    public CyanShadContextMenuRadioItem()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Left;
    }
}

/// <summary>上下文菜单单选组容器控件</summary>
public class CyanShadContextMenuRadioGroup : StackPanel
{
    /// <summary>初始化 CyanShadContextMenuRadioGroup 的新实例</summary>
    public CyanShadContextMenuRadioGroup()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
    }
}