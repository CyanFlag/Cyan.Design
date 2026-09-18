using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Layout;

namespace Cyan.Design.Core.Tests.Controls;

public class CardTests
{
    [AvaloniaFact]
    public void CyanCard_Default_Properties_Should_Be_Expected()
    {
        var card = new CyanCard();
        Assert.Null(card.Header);
        Assert.Null(card.Extra);
        Assert.True(card.Bordered);
        Assert.False(card.Hoverable);
    }

    [AvaloniaFact]
    public void CyanCard_Header_Should_Be_Settable()
    {
        var card = new CyanCard { Header = "Title" };
        Assert.Equal("Title", card.Header);
    }

    [AvaloniaFact]
    public void CyanCard_Extra_Should_Be_Settable()
    {
        var card = new CyanCard { Extra = "More" };
        Assert.Equal("More", card.Extra);
    }

    [AvaloniaFact]
    public void CyanCard_Bordered_Should_Be_Settable()
    {
        var card = new CyanCard { Bordered = false };
        Assert.False(card.Bordered);
    }

    [AvaloniaFact]
    public void CyanCard_Hoverable_Should_Be_Settable()
    {
        var card = new CyanCard { Hoverable = true };
        Assert.True(card.Hoverable);
    }
}
