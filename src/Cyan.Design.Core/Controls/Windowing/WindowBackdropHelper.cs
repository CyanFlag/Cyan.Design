using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace Cyan.Design.Core.Controls.Windowing;

internal static class WindowBackdropHelper
{
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_SYSTEMBACKDROP_TYPE = 38;

    private enum DwmSystembackdropType
    {
        None = 0,
        Auto = 1,
        Mica = 2,
        Acrylic = 3,
        Tabbed = 4
    }

    [DllImport("dwmapi.dll", PreserveSig = false)]
    private static extern void DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    /// <summary>为指定窗口应用背景材质（Mica、Acrylic、Tabbed 等）</summary>
    public static void ApplyBackdrop(Window window, WindowBackdrop backdrop)
    {
        if (!OperatingSystem.IsWindows()) return;
        if (window.TryGetPlatformHandle() is not { } handle) return;

        var value = backdrop switch
        {
            WindowBackdrop.Mica => (int)DwmSystembackdropType.Mica,
            WindowBackdrop.Acrylic => (int)DwmSystembackdropType.Acrylic,
            WindowBackdrop.Tabbed => (int)DwmSystembackdropType.Tabbed,
            _ => (int)DwmSystembackdropType.None
        };

        try
        {
            DwmSetWindowAttribute(handle.Handle, DWMWA_SYSTEMBACKDROP_TYPE, ref value, sizeof(int));
        }
        catch
        {
        }
    }

    /// <summary>为指定窗口应用深色或浅色模式</summary>
    public static void ApplyDarkMode(Window window, bool isDark)
    {
        if (!OperatingSystem.IsWindows()) return;
        if (window.TryGetPlatformHandle() is not { } handle) return;

        var value = isDark ? 1 : 0;
        try
        {
            DwmSetWindowAttribute(handle.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }
        catch
        {
        }
    }
}