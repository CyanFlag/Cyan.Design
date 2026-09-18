using Avalonia.Layout;
using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Display;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanAvatarGroupTest
{
    [AvaloniaFact]
    public void CyanAvatarGroup_Default_Properties_Should_Be_Expected()
    {
        var group = new CyanAvatarGroup();
        Assert.Equal(0, group.MaxCount);
        Assert.Equal(Orientation.Horizontal, group.Orientation);
        Assert.Equal(-8.0, group.Spacing);
    }

    [AvaloniaFact]
    public void CyanAvatarGroup_MaxCount_Should_Be_Settable()
    {
        var group = new CyanAvatarGroup { MaxCount = 3 };
        Assert.Equal(3, group.MaxCount);
    }

    [AvaloniaFact]
    public void CyanAvatarGroup_Should_Be_StackPanel()
    {
        var group = new CyanAvatarGroup();
        Assert.IsAssignableFrom<Avalonia.Controls.StackPanel>(group);
    }
}