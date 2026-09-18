using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Controls.Typography;

/// <summary>段落控件，支持多种类型及加粗、标记样式</summary>
public class CyanParagraph : TextBlock
{
    /// <summary>文本类型样式属性</summary>
    public static readonly StyledProperty<TextType> TypeProperty =
        AvaloniaProperty.Register<CyanParagraph, TextType>(nameof(Type), TextType.Default);

    /// <summary>加粗样式属性</summary>
    public static readonly StyledProperty<bool> StrongProperty =
        AvaloniaProperty.Register<CyanParagraph, bool>(nameof(Strong));

    /// <summary>标记样式属性</summary>
    public static readonly StyledProperty<bool> MarkProperty =
        AvaloniaProperty.Register<CyanParagraph, bool>(nameof(Mark));

    /// <summary>文本类型，决定段落的语义色彩</summary>
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
}
