using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class DrawerTests
{
    [AvaloniaFact]
    public void CyanDrawer_Default_Properties_Should_Be_Expected()
    {
        var drawer = new CyanDrawer();
        Assert.False(drawer.Open);
        Assert.Equal(DrawerPlacement.Right, drawer.Placement);
        Assert.Null(drawer.Title);
        Assert.Null(drawer.Footer);
        Assert.Null(drawer.Extra);
        Assert.True(drawer.Closable);
        Assert.True(drawer.Mask);
        Assert.True(drawer.MaskClosable);
        Assert.Equal(DrawerSize.Default, drawer.Size);
        Assert.Null(drawer.CustomSize);
    }

    [AvaloniaFact]
    public void CyanDrawer_Open_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { Open = true };
        Assert.True(drawer.Open);
    }

    [AvaloniaFact]
    public void CyanDrawer_Placement_Should_Be_Settable()
    {
        var drawer = new CyanDrawer();
        drawer.Placement = DrawerPlacement.Left;
        Assert.Equal(DrawerPlacement.Left, drawer.Placement);

        drawer.Placement = DrawerPlacement.Top;
        Assert.Equal(DrawerPlacement.Top, drawer.Placement);

        drawer.Placement = DrawerPlacement.Bottom;
        Assert.Equal(DrawerPlacement.Bottom, drawer.Placement);
    }

    [AvaloniaFact]
    public void CyanDrawer_Closable_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { Closable = false };
        Assert.False(drawer.Closable);
    }

    [AvaloniaFact]
    public void CyanDrawer_Mask_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { Mask = false };
        Assert.False(drawer.Mask);
    }

    [AvaloniaFact]
    public void CyanDrawer_MaskClosable_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { MaskClosable = false };
        Assert.False(drawer.MaskClosable);
    }

    [AvaloniaFact]
    public void CyanDrawer_Size_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { Size = DrawerSize.Large };
        Assert.Equal(DrawerSize.Large, drawer.Size);
    }

    [AvaloniaFact]
    public void CyanDrawer_CustomSize_Should_Be_Settable()
    {
        var drawer = new CyanDrawer { CustomSize = 500.0 };
        Assert.Equal(500.0, drawer.CustomSize);
    }
}
