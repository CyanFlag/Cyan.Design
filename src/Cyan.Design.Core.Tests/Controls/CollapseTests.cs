using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Display;

namespace Cyan.Design.Core.Tests.Controls;

public class CollapseTests
{
    [AvaloniaFact]
    public void CyanCollapse_Default_Properties_Should_Be_Expected()
    {
        var collapse = new CyanCollapse();
        Assert.False(collapse.Accordion);
        Assert.True(collapse.Bordered);
        Assert.False(collapse.Ghost);
        Assert.Equal(CollapseSize.Medium, collapse.Size);
        Assert.Equal(CollapseIconPlacement.Start, collapse.ExpandIconPlacement);
    }

    [AvaloniaFact]
    public void CyanCollapse_Accordion_Should_Be_Settable()
    {
        var collapse = new CyanCollapse { Accordion = true };
        Assert.True(collapse.Accordion);
    }

    [AvaloniaFact]
    public void CyanCollapse_Bordered_Should_Be_Settable()
    {
        var collapse = new CyanCollapse { Bordered = false };
        Assert.False(collapse.Bordered);
    }

    [AvaloniaFact]
    public void CyanCollapse_Ghost_Should_Be_Settable()
    {
        var collapse = new CyanCollapse { Ghost = true };
        Assert.True(collapse.Ghost);
    }

    [AvaloniaFact]
    public void CyanCollapse_Size_Should_Be_Settable()
    {
        var collapse = new CyanCollapse();
        collapse.Size = CollapseSize.Large;
        Assert.Equal(CollapseSize.Large, collapse.Size);

        collapse.Size = CollapseSize.Small;
        Assert.Equal(CollapseSize.Small, collapse.Size);
    }

    [AvaloniaFact]
    public void CyanCollapse_ExpandIconPlacement_Should_Be_Settable()
    {
        var collapse = new CyanCollapse { ExpandIconPlacement = CollapseIconPlacement.End };
        Assert.Equal(CollapseIconPlacement.End, collapse.ExpandIconPlacement);
    }
}
