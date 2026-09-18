using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Controls.Typography;

/// <summary>标题控件，支持多级标题及标记、代码样式</summary>
public class CyanTitle : TextBlock
{
    /// <summary>标题级别样式属性</summary>
    public static readonly StyledProperty<TitleLevel> LevelProperty =
        AvaloniaProperty.Register<CyanTitle, TitleLevel>(nameof(Level), TitleLevel.H1);

    /// <summary>标记样式属性</summary>
    public static readonly StyledProperty<bool> MarkProperty =
        AvaloniaProperty.Register<CyanTitle, bool>(nameof(Mark));

    /// <summary>代码样式属性</summary>
    public static readonly StyledProperty<bool> CodeProperty =
        AvaloniaProperty.Register<CyanTitle, bool>(nameof(Code));

    /// <summary>标题级别</summary>
    public TitleLevel Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
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
}
