using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Layout;

namespace Cyan.Design.Core.Tests.Controls;

public class SpaceTests
{
    [AvaloniaFact]
    public void CyanSpace_Default_Properties_Should_Be_Expected()
    {
        var space = new CyanSpace();
        Assert.Equal(SpaceDirection.Horizontal, space.Direction);
        Assert.Equal(ControlSize.Middle, space.SpaceSize);
    }

    [AvaloniaFact]
    public void CyanSpace_Direction_Should_Be_Settable()
    {
        var space = new CyanSpace { Direction = SpaceDirection.Vertical };
        Assert.Equal(SpaceDirection.Vertical, space.Direction);
    }

    [AvaloniaFact]
    public void CyanSpace_Size_Should_Be_Settable()
    {
        var space = new CyanSpace();
        space.SpaceSize = ControlSize.Large;
        Assert.Equal(ControlSize.Large, space.SpaceSize);

        space.SpaceSize = ControlSize.Small;
        Assert.Equal(ControlSize.Small, space.SpaceSize);
    }
}
