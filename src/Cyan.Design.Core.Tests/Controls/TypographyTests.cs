using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Typography;

namespace Cyan.Design.Core.Tests.Controls;

public class TypographyTests
{
    [AvaloniaFact]
    public void CyanTitle_Default_Properties_Should_Be_Expected()
    {
        var title = new CyanTitle();
        Assert.Equal(TitleLevel.H1, title.Level);
        Assert.False(title.Mark);
        Assert.False(title.Code);
    }

    [AvaloniaFact]
    public void CyanTitle_Level_Should_Be_Settable()
    {
        var title = new CyanTitle();
        title.Level = TitleLevel.H2;
        Assert.Equal(TitleLevel.H2, title.Level);

        title.Level = TitleLevel.H3;
        Assert.Equal(TitleLevel.H3, title.Level);

        title.Level = TitleLevel.H4;
        Assert.Equal(TitleLevel.H4, title.Level);

        title.Level = TitleLevel.H5;
        Assert.Equal(TitleLevel.H5, title.Level);
    }

    [AvaloniaFact]
    public void CyanTitle_Mark_And_Code_Should_Be_Settable()
    {
        var title = new CyanTitle { Mark = true, Code = true };
        Assert.True(title.Mark);
        Assert.True(title.Code);
    }

    [AvaloniaFact]
    public void CyanText_Default_Properties_Should_Be_Expected()
    {
        var text = new CyanText();
        Assert.Equal(TextType.Default, text.Type);
        Assert.False(text.Strong);
        Assert.False(text.Mark);
        Assert.False(text.Code);
        Assert.False(text.Italic);
        Assert.False(text.Delete);
        Assert.False(text.Underline);
    }

    [AvaloniaFact]
    public void CyanText_Type_Should_Be_Settable()
    {
        var text = new CyanText();
        text.Type = TextType.Secondary;
        Assert.Equal(TextType.Secondary, text.Type);

        text.Type = TextType.Success;
        Assert.Equal(TextType.Success, text.Type);

        text.Type = TextType.Warning;
        Assert.Equal(TextType.Warning, text.Type);

        text.Type = TextType.Danger;
        Assert.Equal(TextType.Danger, text.Type);
    }

    [AvaloniaFact]
    public void CyanText_Style_Flags_Should_Be_Settable()
    {
        var text = new CyanText
        {
            Strong = true,
            Mark = true,
            Code = true,
            Italic = true,
            Delete = true,
            Underline = true
        };
        Assert.True(text.Strong);
        Assert.True(text.Mark);
        Assert.True(text.Code);
        Assert.True(text.Italic);
        Assert.True(text.Delete);
        Assert.True(text.Underline);
    }
}
