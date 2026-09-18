using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Dropdowns;

namespace Cyan.Design.Core.Tests.Controls;

public class DropdownTests
{
    [AvaloniaFact]
    public void CyanDropdown_Default_Properties_Should_Be_Expected()
    {
        var dropdown = new CyanDropdown();
        Assert.Null(dropdown.Overlay);
        Assert.Equal(DropdownTrigger.Hover, dropdown.Trigger);
        Assert.Equal(DropdownPlacement.BottomLeft, dropdown.Placement);
        Assert.False(dropdown.Arrow);
        Assert.False(dropdown.Disabled);
        Assert.False(dropdown.Open);
    }

    [AvaloniaFact]
    public void CyanDropdown_Trigger_Should_Be_Settable()
    {
        var dropdown = new CyanDropdown();
        dropdown.Trigger = DropdownTrigger.Click;
        Assert.Equal(DropdownTrigger.Click, dropdown.Trigger);

        dropdown.Trigger = DropdownTrigger.ContextMenu;
        Assert.Equal(DropdownTrigger.ContextMenu, dropdown.Trigger);
    }

    [AvaloniaFact]
    public void CyanDropdown_Placement_Should_Be_Settable()
    {
        var dropdown = new CyanDropdown { Placement = DropdownPlacement.Top };
        Assert.Equal(DropdownPlacement.Top, dropdown.Placement);
    }

    [AvaloniaFact]
    public void CyanDropdown_Arrow_Should_Be_Settable()
    {
        var dropdown = new CyanDropdown { Arrow = true };
        Assert.True(dropdown.Arrow);
    }

    [AvaloniaFact]
    public void CyanDropdown_Disabled_Should_Be_Settable()
    {
        var dropdown = new CyanDropdown { Disabled = true };
        Assert.True(dropdown.Disabled);
    }

    [AvaloniaFact]
    public void CyanDropdown_Open_Should_Be_Settable()
    {
        var dropdown = new CyanDropdown { Open = true };
        Assert.True(dropdown.Open);
    }
}
