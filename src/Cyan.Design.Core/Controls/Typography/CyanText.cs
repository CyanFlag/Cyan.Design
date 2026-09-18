using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Controls.Typography;

/// <summary>文本控件，支持多种类型及加粗、标记、代码、斜体、删除线、下划线等样式</summary>
public class CyanText : TextBlock
{
    /// <summary>文本类型样式属性</summary>
    public static readonly StyledProperty<TextType> TypeProperty =
        AvaloniaProperty.Register<CyanText, TextType>(nameof(Type), TextType.Default);

    /// <summary>加粗样式属性</summary>
    public static readonly StyledProperty<bool> StrongProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Strong));

    /// <summary>标记样式属性</summary>
    public static readonly StyledProperty<bool> MarkProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Mark));

    /// <summary>代码样式属性</summary>
    public static readonly StyledProperty<bool> CodeProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Code));

    /// <summary>斜体样式属性</summary>
    public static readonly StyledProperty<bool> ItalicProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Italic));

    /// <summary>删除线样式属性</summary>
    public static readonly StyledProperty<bool> DeleteProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Delete));

    /// <summary>下划线样式属性</summary>
    public static readonly StyledProperty<bool> UnderlineProperty =
        AvaloniaProperty.Register<CyanText, bool>(nameof(Underline));

    /// <summary>文本类型，决定文本的语义色彩</summary>
    public TextType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>是否加粗显示</summary>
    public bool Strong
    {
        get => GetValue(StrongProperty);
        set => SetValue(StrongProperty, value);
    }

    /// <summary>是否以标记样式显示</summary>
    public bool Mark
    {
        get => GetValue(MarkProperty);
        set => SetValue(MarkProperty, value);
    }

    /// <summary>是否以代码样式显示</summary>
    public bool Code
    {
        get => GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    /// <summary>是否以斜体显示</summary>
    public bool Italic
    {
        get => GetValue(ItalicProperty);
        set => SetValue(ItalicProperty, value);
    }

    /// <summary>是否以删除线显示</summary>
    public bool Delete
    {
        get => GetValue(DeleteProperty);
        set => SetValue(DeleteProperty, value);
    }

    /// <summary>是否以下划线显示</summary>
    public bool Underline
    {
        get => GetValue(UnderlineProperty);
        set => SetValue(UnderlineProperty, value);
    }
}
