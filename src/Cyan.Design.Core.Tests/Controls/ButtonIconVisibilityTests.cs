using System.IO;
using System.Text.RegularExpressions;

namespace Cyan.Design.Core.Tests.Controls;

public class ButtonIconVisibilityTests
{
    private static string FindThemesRoot()
    {
        var dir = AppContext.BaseDirectory;
        while (dir != null && !Directory.Exists(Path.Combine(dir, "src", "Cyan.Design.Themes")))
        {
            dir = Path.GetDirectoryName(dir);
        }
        return dir != null ? Path.Combine(dir, "src", "Cyan.Design.Themes") : throw new DirectoryNotFoundException("Could not find Themes root.");
    }

    private static string ReadStylesFile(string relativePath)
    {
        var root = FindThemesRoot();
        return File.ReadAllText(Path.Combine(root, relativePath));
    }

    private static string ExtractStyleBlock(string content, string selector)
    {
        var pattern = $@"<Style\s+Selector=""{Regex.Escape(selector)}""[^>]*>(.*?)</Style>";
        var match = Regex.Match(content, pattern, RegexOptions.Singleline | RegexOptions.Compiled);
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    [Fact]
    public void Button_axaml_Should_Not_Contain_Hardcoded_White()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Button.axaml"));
        Assert.DoesNotContain("Value=\"White\"", content);
    }

    [Fact]
    public void Button_axaml_Primary_Foreground_Should_Reference_ColorTextOnPrimaryBrush()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Button.axaml"));
        var block = ExtractStyleBlock(content, "core|CyanButton[ButtonType=Primary]");
        Assert.Contains("ColorTextOnPrimaryBrush", block);
        Assert.Contains("Property=\"Foreground\"", block);
    }

    [Fact]
    public void Button_axaml_PrimaryDanger_Foreground_Should_Reference_ColorTextOnDangerBrush()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Button.axaml"));
        var block = ExtractStyleBlock(content, "core|CyanButton[ButtonType=Primary][Danger=True]");
        Assert.Contains("ColorTextOnDangerBrush", block);
        Assert.Contains("Property=\"Foreground\"", block);
    }

    [Fact]
    public void Button_axaml_Ghost_Foreground_Should_Not_Reference_ColorBgContainerBrush()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Button.axaml"));
        var block = ExtractStyleBlock(content, "core|CyanButton[Ghost=True]");
        var foregroundMatch = Regex.Match(block, @"Property=""Foreground""\s+Value=""([^""]+)""");
        Assert.True(foregroundMatch.Success, "Ghost style should have a Foreground setter");
        Assert.DoesNotContain("ColorBgContainerBrush", foregroundMatch.Groups[1].Value);
    }

    [Fact]
    public void Button_axaml_Ghost_Foreground_Should_Reference_ColorTextBrush()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Button.axaml"));
        var block = ExtractStyleBlock(content, "core|CyanButton[Ghost=True]");
        var foregroundMatch = Regex.Match(block, @"Property=""Foreground""\s+Value=""([^""]+)""");
        Assert.True(foregroundMatch.Success, "Ghost style should have a Foreground setter");
        Assert.Contains("ColorTextBrush", foregroundMatch.Groups[1].Value);
    }

    [Fact]
    public void Icon_axaml_Should_Not_Set_Foreground_Explicitly()
    {
        var content = ReadStylesFile(Path.Combine("Styles", "Controls", "Icon.axaml"));
        Assert.DoesNotContain("Property=\"Foreground\"", content);
    }
}
