using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanMessageItemTest
{
    [AvaloniaFact]
    public void CyanMessageItem_Default_Properties_Should_Be_Expected()
    {
        var msg = new CyanMessageItem();
        Assert.Null(msg.Text);
        Assert.Equal(MessageType.Info, msg.MessageType);
    }

    [AvaloniaFact]
    public void CyanMessageItem_Text_Should_Be_Settable()
    {
        var msg = new CyanMessageItem { Text = "Saved" };
        Assert.Equal("Saved", msg.Text);
    }

    [AvaloniaFact]
    public void CyanMessageItem_MessageType_Should_Be_Settable()
    {
        var msg = new CyanMessageItem { MessageType = MessageType.Error };
        Assert.Equal(MessageType.Error, msg.MessageType);
    }

    [AvaloniaFact]
    public void CyanMessageItem_All_Types_Should_Be_Settable()
    {
        var msg = new CyanMessageItem();
        foreach (var type in new[] { MessageType.Success, MessageType.Info, MessageType.Warning, MessageType.Error, MessageType.Loading })
        {
            msg.MessageType = type;
            Assert.Equal(type, msg.MessageType);
        }
    }
}