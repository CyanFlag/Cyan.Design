using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Display;

namespace Cyan.Design.Core.Tests.Controls;

public class AvatarTests
{
    [AvaloniaFact]
    public void CyanAvatar_Default_Properties_Should_Be_Expected()
    {
        var avatar = new CyanAvatar();
        Assert.Null(avatar.Source);
        Assert.Null(avatar.Text);
        Assert.Equal(AvatarShape.Circle, avatar.Shape);
        Assert.Equal(AvatarSize.Medium, avatar.Size);
        Assert.Equal(4, avatar.Gap);
    }

    [AvaloniaFact]
    public void CyanAvatar_Text_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { Text = "AB" };
        Assert.Equal("AB", avatar.Text);
    }

    [AvaloniaFact]
    public void CyanAvatar_Shape_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { Shape = AvatarShape.Square };
        Assert.Equal(AvatarShape.Square, avatar.Shape);
    }

    [AvaloniaFact]
    public void CyanAvatar_Size_Should_Be_Settable()
    {
        var avatar = new CyanAvatar();
        avatar.Size = AvatarSize.Large;
        Assert.Equal(AvatarSize.Large, avatar.Size);

        avatar.Size = AvatarSize.Small;
        Assert.Equal(AvatarSize.Small, avatar.Size);
    }

    [AvaloniaFact]
    public void CyanAvatar_Gap_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { Gap = 8 };
        Assert.Equal(8, avatar.Gap);
    }

    [AvaloniaFact]
    public void CyanAvatar_Alt_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { Alt = "avatar" };
        Assert.Equal("avatar", avatar.Alt);
    }

    [AvaloniaFact]
    public void CyanAvatar_BadgeText_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { BadgeText = "New" };
        Assert.Equal("New", avatar.BadgeText);
    }

    [AvaloniaFact]
    public void CyanAvatar_BadgeDot_Should_Be_Settable()
    {
        var avatar = new CyanAvatar { BadgeDot = true };
        Assert.True(avatar.BadgeDot);
    }
}
