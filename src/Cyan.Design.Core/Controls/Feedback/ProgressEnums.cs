namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>进度条类型</summary>
public enum ProgressType
{
    /// <summary>线性进度条</summary>
    Line,
    /// <summary>圆形进度条</summary>
    Circle,
    /// <summary>仪表盘进度条</summary>
    Dashboard
}

/// <summary>进度条状态</summary>
public enum ProgressStatus
{
    /// <summary>正常状态</summary>
    Normal,
    /// <summary>活跃状态</summary>
    Active,
    /// <summary>成功状态</summary>
    Success,
    /// <summary>异常状态</summary>
    Exception
}


/// <summary>进度条线帽样式</summary>
public enum ProgressStrokeLinecap
{
    /// <summary>圆角线帽</summary>
    Round,
    /// <summary>平直线帽</summary>
    Butt
}

/// <summary>进度信息位置</summary>
public enum ProgressInfoPosition
{
    /// <summary>右侧</summary>
    Right,
    /// <summary>顶部</summary>
    Top,
    /// <summary>底部</summary>
    Bottom,
    /// <summary>内部</summary>
    Inside,
    /// <summary>居中</summary>
    Center,
    /// <summary>跟随</summary>
    Follow
}
