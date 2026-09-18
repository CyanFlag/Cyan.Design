using Avalonia.Headless.XUnit;
using Avalonia.Layout;
using Cyan.Design.Core.Controls.Layout;

namespace Cyan.Design.Core.Tests.Controls;

public class DividerTests
{
    [AvaloniaFact]
    public void CyanDivider_Default_Properties_Should_Be_Expected()
    {
        var divider = new CyanDivider();
        Assert.Equal(Orientation.Horizontal, divider.Orientation);
        Assert.False(divider.Dashed);
        Assert.Equal(DividerAlign.Center, divider.Align);
    }

    [AvaloniaFact]
    public void CyanDivider_Orientation_Should_Be_Settable()
    {
        var divider = new CyanDivider { Orientation = Orientation.Vertical };
        Assert.Equal(Orientation.Vertical, divider.Orientation);
    }

    [AvaloniaFact]
    public void CyanDivider_Dashed_Should_Be_Settable()
    {
        var divider = new CyanDivider { Dashed = true };
        Assert.True(divider.Dashed);
    }

    [AvaloniaFact]
    public void CyanDivider_Align_Should_Be_Settable()
    {
        var divider = new CyanDivider();
        divider.Align = DividerAlign.Left;
        Assert.Equal(DividerAlign.Left, divider.Align);

        divider.Align = DividerAlign.Right;
        Assert.Equal(DividerAlign.Right, divider.Align);
    }
}
