using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Display;

namespace Cyan.Design.Core.Tests.Controls;

public class BadgeTests
{
    [AvaloniaFact]
    public void CyanBadge_Default_Properties_Should_Be_Expected()
    {
        var badge = new CyanBadge();
        Assert.Equal(0, badge.Count);
        Assert.Equal(99, badge.OverflowCount);
        Assert.False(badge.ShowZero);
        Assert.False(badge.Dot);
        Assert.Equal(BadgeStatus.None, badge.Status);
        Assert.Equal(BadgeSize.Default, badge.Size);
    }

    [AvaloniaFact]
    public void CyanBadge_Count_Should_Be_Settable()
    {
        var badge = new CyanBadge { Count = 42 };
        Assert.Equal(42, badge.Count);
    }

    [AvaloniaFact]
    public void CyanBadge_OverflowCount_Should_Be_Settable()
    {
        var badge = new CyanBadge { OverflowCount = 200 };
        Assert.Equal(200, badge.OverflowCount);
    }

    [AvaloniaFact]
    public void CyanBadge_ShowZero_Should_Be_Settable()
    {
        var badge = new CyanBadge { ShowZero = true };
        Assert.True(badge.ShowZero);
    }

    [AvaloniaFact]
    public void CyanBadge_Dot_Should_Be_Settable()
    {
        var badge = new CyanBadge { Dot = true };
        Assert.True(badge.Dot);
    }

    [AvaloniaFact]
    public void CyanBadge_Status_Should_Be_Settable()
    {
        var badge = new CyanBadge();
        badge.Status = BadgeStatus.Success;
        Assert.Equal(BadgeStatus.Success, badge.Status);

        badge.Status = BadgeStatus.Error;
        Assert.Equal(BadgeStatus.Error, badge.Status);

        badge.Status = BadgeStatus.Warning;
        Assert.Equal(BadgeStatus.Warning, badge.Status);
    }

    [AvaloniaFact]
    public void CyanBadge_Size_Should_Be_Settable()
    {
        var badge = new CyanBadge { Size = BadgeSize.Small };
        Assert.Equal(BadgeSize.Small, badge.Size);
    }
}
