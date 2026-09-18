using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>抽屉控件，从屏幕边缘滑出的浮层容器</summary>
public class CyanDrawer : ContentControl
{
    /// <summary>是否打开抽屉的样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanDrawer, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>抽屉弹出方向的样式属性</summary>
    public static readonly StyledProperty<DrawerPlacement> PlacementProperty =
        AvaloniaProperty.Register<CyanDrawer, DrawerPlacement>(nameof(Placement), DrawerPlacement.Right);

    /// <summary>抽屉标题的样式属性</summary>
    public static readonly StyledProperty<object?> TitleProperty =
        AvaloniaProperty.Register<CyanDrawer, object?>(nameof(Title));

    /// <summary>底部内容的样式属性</summary>
    public static readonly StyledProperty<object?> FooterProperty =
        AvaloniaProperty.Register<CyanDrawer, object?>(nameof(Footer));

    /// <summary>额外内容的样式属性</summary>
    public static readonly StyledProperty<object?> ExtraProperty =
        AvaloniaProperty.Register<CyanDrawer, object?>(nameof(Extra));

    /// <summary>是否显示关闭按钮的样式属性</summary>
    public static readonly StyledProperty<bool> ClosableProperty =
        AvaloniaProperty.Register<CyanDrawer, bool>(nameof(Closable), true);

    /// <summary>是否显示遮罩层的样式属性</summary>
    public static readonly StyledProperty<bool> MaskProperty =
        AvaloniaProperty.Register<CyanDrawer, bool>(nameof(Mask), true);

    /// <summary>点击遮罩层是否允许关闭抽屉的样式属性</summary>
    public static readonly StyledProperty<bool> MaskClosableProperty =
        AvaloniaProperty.Register<CyanDrawer, bool>(nameof(MaskClosable), true);

    /// <summary>抽屉尺寸的样式属性</summary>
    public static readonly StyledProperty<DrawerSize> SizeProperty =
        AvaloniaProperty.Register<CyanDrawer, DrawerSize>(nameof(Size), DrawerSize.Default);

    /// <summary>自定义尺寸的样式属性</summary>
    public static readonly StyledProperty<double?> CustomSizeProperty =
        AvaloniaProperty.Register<CyanDrawer, double?>(nameof(CustomSize));

    static CyanDrawer()
    {
        OpenProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
        PlacementProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => { d.UpdatePseudoClasses(); d.UpdateSize(); });
        SizeProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => { d.UpdatePseudoClasses(); d.UpdateSize(); });
        CustomSizeProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdateSize());
        FooterProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
        ExtraProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
        TitleProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
        MaskProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
        ClosableProperty.Changed.AddClassHandler<CyanDrawer>((d, _) => d.UpdatePseudoClasses());
    }

    /// <summary>是否打开抽屉</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>抽屉弹出方向</summary>
    public DrawerPlacement Placement
    {
        get => GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>抽屉标题</summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>底部内容</summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>额外内容</summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>是否显示关闭按钮</summary>
    public bool Closable
    {
        get => GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>是否显示遮罩层</summary>
    public bool Mask
    {
        get => GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }

    /// <summary>点击遮罩层是否允许关闭抽屉</summary>
    public bool MaskClosable
    {
        get => GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
    }

    /// <summary>抽屉尺寸</summary>
    public DrawerSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>自定义尺寸</summary>
    public double? CustomSize
    {
        get => GetValue(CustomSizeProperty);
        set => SetValue(CustomSizeProperty, value);
    }

    private Border? _section;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        var mask = e.NameScope.Find<Border>("PART_Mask");
        if (mask != null)
            mask.PointerPressed += OnMaskPointerPressed;

        var close = e.NameScope.Find<Control>("PART_Close");
        if (close != null)
            close.PointerPressed += OnClosePointerPressed;

        _section = e.NameScope.Find<Border>("PART_Section");
        UpdateSize();
    }

    private void OnMaskPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (MaskClosable)
            Open = false;
    }

    private void OnClosePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Open = false;
        e.Handled = true;
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }

    private void UpdatePseudoClasses()
    {
        IsVisible = Open;
        PseudoClasses.Set(":open", Open);
        PseudoClasses.Set(":right", Placement == DrawerPlacement.Right);
        PseudoClasses.Set(":left", Placement == DrawerPlacement.Left);
        PseudoClasses.Set(":top", Placement == DrawerPlacement.Top);
        PseudoClasses.Set(":bottom", Placement == DrawerPlacement.Bottom);
        PseudoClasses.Set(":large", Size == DrawerSize.Large);
        PseudoClasses.Set(":has-footer", Footer != null);
        PseudoClasses.Set(":has-extra", Extra != null);
        PseudoClasses.Set(":has-title", Title != null);
        PseudoClasses.Set(":no-mask", !Mask);
        PseudoClasses.Set(":no-closable", !Closable);
    }

    private void UpdateSize()
    {
        if (_section == null) return;
        double size = CustomSize ?? (Size == DrawerSize.Large ? 736 : 378);
        if (Placement == DrawerPlacement.Left || Placement == DrawerPlacement.Right)
        {
            _section.Width = size;
            _section.Height = double.NaN;
        }
        else
        {
            _section.Height = size;
            _section.Width = double.NaN;
        }
    }
}
