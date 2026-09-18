namespace Cyan.Design.Core.Controls.Display;

/// <summary>折叠面板尺寸</summary>
public enum CollapseSize
{
    /// <summary>大尺寸</summary>
    Large,
    /// <summary>中等尺寸</summary>
    Medium,
    /// <summary>小尺寸</summary>
    Small
}

/// <summary>折叠图标位置</summary>
public enum CollapseIconPlacement
{
    /// <summary>起始位置</summary>
    Start,
    /// <summary>末尾位置</summary>
    End
}

/// <summary>折叠可折叠触发方式</summary>
public enum CollapseCollapsible
{
    /// <summary>点击头部触发</summary>
    Header,
    /// <summary>点击图标触发</summary>
    Icon,
    /// <summary>禁用折叠</summary>
    Disabled
}
