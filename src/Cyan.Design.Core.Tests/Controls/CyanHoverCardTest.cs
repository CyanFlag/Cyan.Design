using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Overlays;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanHoverCardTest
{
    [AvaloniaFact]
    public void CyanHoverCard_Default_Properties_Should_Be_Expected()
    {
        var card = new CyanHoverCard();
        Assert.Null(card.Overlay);
        Assert.Equal(HoverCardPlacement.Top, card.Placement);
        Assert.True(card.Arrow);
        Assert.False(card.Disabled);
        Assert.False(card.Open);
        Assert.Equal(600, card.OpenDelay);
        Assert.Equal(300, card.CloseDelay);
        Assert.Equal(320.0, card.OverlayMaxWidth);
    }

    [AvaloniaFact]
    public void CyanHoverCard_Placement_Should_Be_Settable()
    {
        var card = new CyanHoverCard { Placement = HoverCardPlacement.Bottom };
        Assert.Equal(HoverCardPlacement.Bottom, card.Placement);
    }

    [AvaloniaFact]
    public void CyanHoverCard_Arrow_Should_Be_Settable()
    {
        var card = new CyanHoverCard { Arrow = false };
        Assert.False(card.Arrow);
    }

    [AvaloniaFact]
    public void CyanHoverCard_Disabled_Should_Be_Settable()
    {
        var card = new CyanHoverCard { Disabled = true };
        Assert.True(card.Disabled);
    }

    [AvaloniaFact]
    public void CyanHoverCard_Open_Should_Be_Settable()
    {
        var card = new CyanHoverCard { Open = true };
        Assert.True(card.Open);
    }

    [AvaloniaFact]
    public void CyanHoverCard_OpenDelay_Should_Be_Settable()
    {
        var card = new CyanHoverCard { OpenDelay = 1000 };
        Assert.Equal(1000, card.OpenDelay);
    }

    [AvaloniaFact]
    public void CyanHoverCard_CloseDelay_Should_Be_Settable()
    {
        var card = new CyanHoverCard { CloseDelay = 500 };
        Assert.Equal(500, card.CloseDelay);
    }

    [AvaloniaFact]
    public void CyanHoverCard_OverlayMaxWidth_Should_Be_Settable()
    {
        var card = new CyanHoverCard { OverlayMaxWidth = 400 };
        Assert.Equal(400.0, card.OverlayMaxWidth);
    }
}