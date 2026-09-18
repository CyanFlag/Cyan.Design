namespace Cyan.Design.Core.Controls.Inputs;


/// <summary>输入框状态</summary>
public enum InputStatus
{
    /// <summary>默认状态</summary>
    Default,
    /// <summary>错误状态</summary>
    Error,
    /// <summary>警告状态</summary>
    Warning
}

/// <summary>输入框变体样式</summary>
public enum InputVariant
{
    /// <summary>描边样式</summary>
    Outlined,
    /// <summary>填充样式</summary>
    Filled,
    /// <summary>无边框样式</summary>
    Borderless,
    /// <summary>下划线样式</summary>
    Underlined
}

/// <summary>步进控制器可见性</summary>
public enum HandleVisible
{
    /// <summary>始终可见</summary>
    Always,
    /// <summary>自动显示</summary>
    Auto
}

/// <summary>数字输入框步进模式</summary>
public enum NumberControlMode
{
    /// <summary>垂直步进</summary>
    Vertical,
    /// <summary>水平步进</summary>
    Horizontal
}

/// <summary>步进类型</summary>
public enum StepType
{
    /// <summary>增加</summary>
    Up,
    /// <summary>减少</summary>
    Down
}

/// <summary>步进触发来源</summary>
public enum StepEmitter
{
    /// <summary>按钮触发</summary>
    Handler,
    /// <summary>键盘触发</summary>
    KeyDown,
    /// <summary>滚轮触发</summary>
    Wheel
}
