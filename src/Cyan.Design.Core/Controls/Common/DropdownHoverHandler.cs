using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Common;

/// <summary>
/// 提供 Dropdown 与 Popconfirm 共享的 Hover/Click 触发与 Popup 事件挂载逻辑。
/// </summary>
public static class DropdownHoverHandler
{
    /// <summary>
    /// 为 Popup 的 Child 挂载 PointerEntered/Exited 事件处理器。
    /// </summary>
    public static void AttachPopupChildHandlers(Popup popup, EventHandler<PointerEventArgs> handler)
    {
        if (popup.Child is { } popupChild)
        {
            popupChild.PointerEntered += handler;
            popupChild.PointerExited += handler;
        }
    }

    /// <summary>
    /// 从 Popup 的 Child 卸载 PointerEntered/Exited 事件处理器。
    /// </summary>
    public static void DetachPopupChildHandlers(Popup? popup, EventHandler<PointerEventArgs> handler)
    {
        if (popup?.Child is { } oldChild)
        {
            oldChild.PointerEntered -= handler;
            oldChild.PointerExited -= handler;
        }
    }

    /// <summary>
    /// 调度延迟判定 Hover 关闭：当鼠标不在宿主也不在 Popup 内时执行关闭回调。
    /// </summary>
    public static void ScheduleHoverClose(Popup? popup, bool hovering, Action closeCallback)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (!hovering && popup?.IsPointerOverPopup != true)
                closeCallback();
        });
    }
}