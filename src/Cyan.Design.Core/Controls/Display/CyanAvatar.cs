using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Display;

/// <summary>头像控件，支持图片、文本、图标显示</summary>
public class CyanAvatar : TemplatedControl
{
    /// <summary>图像源样式属性</summary>
    public static readonly StyledProperty<IImage?> SourceProperty =
        AvaloniaProperty.Register<CyanAvatar, IImage?>(nameof(Source));

    /// <summary>文本内容样式属性</summary>
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<CyanAvatar, string?>(nameof(Text));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanAvatar, object?>(nameof(Icon));

    /// <summary>头像形状样式属性</summary>
    public static readonly StyledProperty<AvatarShape> ShapeProperty =
        AvaloniaProperty.Register<CyanAvatar, AvatarShape>(nameof(Shape), AvatarShape.Circle);

    /// <summary>头像尺寸样式属性</summary>
    public static readonly StyledProperty<AvatarSize> SizeProperty =
        AvaloniaProperty.Register<CyanAvatar, AvatarSize>(nameof(Size), AvatarSize.Medium);

    /// <summary>字符头像间距样式属性</summary>
    public static readonly StyledProperty<int> GapProperty =
        AvaloniaProperty.Register<CyanAvatar, int>(nameof(Gap), 4);

    /// <summary>替代文本样式属性</summary>
    public static readonly StyledProperty<string?> AltProperty =
        AvaloniaProperty.Register<CyanAvatar, string?>(nameof(Alt));

    /// <summary>徽标文本样式属性</summary>
    public static readonly StyledProperty<string?> BadgeTextProperty =
        AvaloniaProperty.Register<CyanAvatar, string?>(nameof(BadgeText));

    /// <summary>徽标圆点样式属性</summary>
    public static readonly StyledProperty<bool> BadgeDotProperty =
        AvaloniaProperty.Register<CyanAvatar, bool>(nameof(BadgeDot));

    /// <summary>徽标颜色样式属性</summary>
    public static readonly StyledProperty<IBrush?> BadgeColorProperty =
        AvaloniaProperty.Register<CyanAvatar, IBrush?>(nameof(BadgeColor));

    static CyanAvatar()
    {
        SourceProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => a.UpdateContentPseudoClasses());
        TextProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => a.UpdateContentPseudoClasses());
        IconProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => a.UpdateContentPseudoClasses());
        ShapeProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => UpdateShapeSizePseudoClasses(a));
        SizeProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => UpdateShapeSizePseudoClasses(a));
        BadgeTextProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => a.UpdateBadgePseudoClasses());
        BadgeDotProperty.Changed.AddClassHandler<CyanAvatar>((a, _) => a.UpdateBadgePseudoClasses());
    }

    /// <summary>图像源，用于显示图片头像</summary>
    public IImage? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>文本内容，用于显示字符头像</summary>
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>图标，用于显示图标头像</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>头像形状</summary>
    public AvatarShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>头像尺寸</summary>
    public AvatarSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>字符头像间距</summary>
    public int Gap
    {
        get => GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }

    /// <summary>替代文本，用于图片加载失败时显示</summary>
    public string? Alt
    {
        get => GetValue(AltProperty);
        set => SetValue(AltProperty, value);
    }

    /// <summary>徽标文本</summary>
    public string? BadgeText
    {
        get => GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    /// <summary>是否显示徽标圆点</summary>
    public bool BadgeDot
    {
        get => GetValue(BadgeDotProperty);
        set => SetValue(BadgeDotProperty, value);
    }

    /// <summary>徽标颜色</summary>
    public IBrush? BadgeColor
    {
        get => GetValue(BadgeColorProperty);
        set => SetValue(BadgeColorProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdateShapeSizePseudoClasses(this);
        UpdateContentPseudoClasses();
        UpdateBadgePseudoClasses();
    }

    private static void UpdateShapeSizePseudoClasses(CyanAvatar a)
    {
        a.PseudoClasses.Set(":circle", a.Shape == AvatarShape.Circle);
        a.PseudoClasses.Set(":square", a.Shape == AvatarShape.Square);
        a.PseudoClasses.Set(":large", a.Size == AvatarSize.Large);
        a.PseudoClasses.Set(":small", a.Size == AvatarSize.Small);
    }

    private void UpdateContentPseudoClasses()
    {
        bool hasImage = Source != null;
        bool hasIcon = Icon != null;
        bool hasText = !string.IsNullOrEmpty(Text);
        PseudoClasses.Set(":image", hasImage);
        PseudoClasses.Set(":icon", !hasImage && hasIcon);
        PseudoClasses.Set(":text", !hasImage && !hasIcon && hasText);
    }

    private void UpdateBadgePseudoClasses()
    {
        bool hasBadgeText = !string.IsNullOrEmpty(BadgeText);
        bool hasBadgeDot = BadgeDot;
        bool hasBadge = hasBadgeText || hasBadgeDot;
        PseudoClasses.Set(":badge", hasBadge);
        PseudoClasses.Set(":badgedot", hasBadge && hasBadgeDot && !hasBadgeText);
        PseudoClasses.Set(":badgetext", hasBadge && hasBadgeText);
    }
}
