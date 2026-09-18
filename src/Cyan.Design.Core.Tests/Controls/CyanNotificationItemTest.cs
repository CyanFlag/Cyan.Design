using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanNotificationItemTest
{
    [AvaloniaFact]
    public void CyanNotificationItem_Default_Properties_Should_Be_Expected()
    {
        var n = new CyanNotificationItem();
        Assert.Null(n.Title);
        Assert.Null(n.Description);
        Assert.Equal(NotificationType.Info, n.NotificationType);
    }

    [AvaloniaFact]
    public void CyanNotificationItem_Title_Should_Be_Settable()
    {
        var n = new CyanNotificationItem { Title = "Alert" };
        Assert.Equal("Alert", n.Title);
    }

    [AvaloniaFact]
    public void CyanNotificationItem_Description_Should_Be_Settable()
    {
        var n = new CyanNotificationItem { Description = "Details" };
        Assert.Equal("Details", n.Description);
    }

    [AvaloniaFact]
    public void CyanNotificationItem_NotificationType_Should_Be_Settable()
    {
        var n = new CyanNotificationItem { NotificationType = NotificationType.Warning };
        Assert.Equal(NotificationType.Warning, n.NotificationType);
    }

    [AvaloniaFact]
    public void CyanNotificationItem_All_Types_Should_Be_Settable()
    {
        var n = new CyanNotificationItem();
        foreach (var type in new[] { NotificationType.Success, NotificationType.Info, NotificationType.Warning, NotificationType.Error })
        {
            n.NotificationType = type;
            Assert.Equal(type, n.NotificationType);
        }
    }
}