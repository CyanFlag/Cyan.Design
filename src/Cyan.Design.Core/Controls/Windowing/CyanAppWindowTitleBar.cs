using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Windowing;

/// <summary>应用窗口标题栏配置，管理标题栏颜色与按钮外观等设置</summary>
public class CyanAppWindowTitleBar
{
    internal CyanAppWindowTitleBar(CyanAppWindow parent)
    {
        _parent = parent;
    }

    /// <summary>标题栏背景色</summary>
    public Color? BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor != value)
            {
                _backgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏前景色（文本颜色）</summary>
    public Color? ForegroundColor
    {
        get => _foregroundColor;
        set
        {
            if (_foregroundColor != value)
            {
                _foregroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>窗口非活动状态时标题栏背景色</summary>
    public Color? InactiveBackgroundColor
    {
        get => _inactiveBackgroundColor;
        set
        {
            if (_inactiveBackgroundColor != value)
            {
                _inactiveBackgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>窗口非活动状态时标题栏前景色</summary>
    public Color? InactiveForegroundColor
    {
        get => _inactiveForegroundColor;
        set
        {
            if (_inactiveForegroundColor != value)
            {
                _inactiveForegroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮背景色</summary>
    public Color? ButtonBackgroundColor
    {
        get => _buttonBackgroundColor;
        set
        {
            if (_buttonBackgroundColor != value)
            {
                _buttonBackgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮前景色</summary>
    public Color? ButtonForegroundColor
    {
        get => _buttonForegroundColor;
        set
        {
            if (_buttonForegroundColor != value)
            {
                _buttonForegroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮悬停时的背景色</summary>
    public Color? ButtonHoverBackgroundColor
    {
        get => _buttonHoverBackgroundColor;
        set
        {
            if (_buttonHoverBackgroundColor != value)
            {
                _buttonHoverBackgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮悬停时的前景色</summary>
    public Color? ButtonHoverForegroundColor
    {
        get => _buttonHoverForegroundColor;
        set
        {
            if (_buttonHoverForegroundColor != value)
            {
                _buttonHoverForegroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮按下时的背景色</summary>
    public Color? ButtonPressedBackgroundColor
    {
        get => _buttonPressedBackgroundColor;
        set
        {
            if (_buttonPressedBackgroundColor != value)
            {
                _buttonPressedBackgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>标题栏按钮按下时的前景色</summary>
    public Color? ButtonPressedForegroundColor
    {
        get => _buttonPressedForegroundColor;
        set
        {
            if (_buttonPressedForegroundColor != value)
            {
                _buttonPressedForegroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>窗口非活动状态时标题栏按钮背景色</summary>
    public Color? ButtonInactiveBackgroundColor
    {
        get => _buttonInactiveBackgroundColor;
        set
        {
            if (_buttonInactiveBackgroundColor != value)
            {
                _buttonInactiveBackgroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>窗口非活动状态时标题栏按钮前景色</summary>
    public Color? ButtonInactiveForegroundColor
    {
        get => _buttonInactiveForegroundColor;
        set
        {
            if (_buttonInactiveForegroundColor != value)
            {
                _buttonInactiveForegroundColor = value;
                _parent.TitleBarColorsChanged();
            }
        }
    }

    /// <summary>是否将内容延伸到标题栏区域</summary>
    public bool ExtendsContentIntoTitleBar
    {
        get => _extendsContentIntoTitleBar;
        set
        {
            if (_extendsContentIntoTitleBar != value)
            {
                _extendsContentIntoTitleBar = value;
                _parent.OnExtendsContentIntoTitleBarChanged(value);
            }
        }
    }

    /// <summary>标题栏高度</summary>
    public double Height
    {
        get => _height;
        set
        {
            if (Math.Abs(_height - value) > 0.001)
            {
                _height = value;
                _parent.OnTitleBarHeightChanged(value);
            }
        }
    }

    /// <summary>是否显示全屏按钮</summary>
    public bool ShowFullScreenButton
    {
        get => _showFullScreenButton;
        set
        {
            if (_showFullScreenButton != value)
            {
                _showFullScreenButton = value;
                _parent.OnShowFullScreenButtonChanged(value);
            }
        }
    }

    private readonly CyanAppWindow _parent;
    private Color? _backgroundColor;
    private Color? _buttonBackgroundColor;
    private Color? _buttonForegroundColor;
    private Color? _buttonHoverBackgroundColor;
    private Color? _buttonHoverForegroundColor;
    private Color? _buttonInactiveBackgroundColor;
    private Color? _buttonInactiveForegroundColor;
    private Color? _buttonPressedBackgroundColor;
    private Color? _buttonPressedForegroundColor;
    private bool _extendsContentIntoTitleBar;
    private Color? _foregroundColor;
    private double _height = 32;
    private Color? _inactiveBackgroundColor;
    private Color? _inactiveForegroundColor;
    private bool _showFullScreenButton = true;
}
