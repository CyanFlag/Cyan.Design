using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Core.Tests.Controls;

public class SegmentedTests
{
    [AvaloniaFact]
    public void CyanSegmented_Default_Properties_Should_Be_Expected()
    {
        var segmented = new CyanSegmented();
        Assert.Null(segmented.SelectedValue);
        Assert.Equal(ControlSize.Middle, segmented.Size);
        Assert.Equal(SegmentedOrientation.Horizontal, segmented.Orientation);
        Assert.False(segmented.Block);
        Assert.Equal(SegmentedShape.Default, segmented.Shape);
        Assert.False(segmented.Disabled);
    }

    [AvaloniaFact]
    public void CyanSegmented_Size_Should_Be_Settable()
    {
        var segmented = new CyanSegmented();
        segmented.Size = ControlSize.Large;
        Assert.Equal(ControlSize.Large, segmented.Size);

        segmented.Size = ControlSize.Small;
        Assert.Equal(ControlSize.Small, segmented.Size);
    }

    [AvaloniaFact]
    public void CyanSegmented_Orientation_Should_Be_Settable()
    {
        var segmented = new CyanSegmented { Orientation = SegmentedOrientation.Vertical };
        Assert.Equal(SegmentedOrientation.Vertical, segmented.Orientation);
    }

    [AvaloniaFact]
    public void CyanSegmented_Block_Should_Be_Settable()
    {
        var segmented = new CyanSegmented { Block = true };
        Assert.True(segmented.Block);
    }

    [AvaloniaFact]
    public void CyanSegmented_Shape_Should_Be_Settable()
    {
        var segmented = new CyanSegmented { Shape = SegmentedShape.Round };
        Assert.Equal(SegmentedShape.Round, segmented.Shape);
    }

    [AvaloniaFact]
    public void CyanSegmented_Disabled_Should_Be_Settable()
    {
        var segmented = new CyanSegmented { Disabled = true };
        Assert.True(segmented.Disabled);
    }

    [AvaloniaFact]
    public void CyanSegmented_SelectedValue_Should_Be_Settable()
    {
        var segmented = new CyanSegmented { SelectedValue = "Option1" };
        Assert.Equal("Option1", segmented.SelectedValue);
    }
}
