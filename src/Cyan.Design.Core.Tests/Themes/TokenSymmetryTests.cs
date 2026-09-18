using System.IO;
using System.Text.RegularExpressions;

namespace Cyan.Design.Core.Tests.Themes;

public class TokenSymmetryTests
{
    private static readonly Regex KeyRegex = new(@"x:Key=""([^""]+)""", RegexOptions.Compiled);
    private static readonly Regex ColorRegex = new(@"Color=""(#[0-9A-Fa-f]{8})""", RegexOptions.Compiled);

    private static string FindThemesRoot()
    {
        var dir = AppContext.BaseDirectory;
        while (dir != null && !Directory.Exists(Path.Combine(dir, "src", "Cyan.Design.Themes")))
        {
            dir = Path.GetDirectoryName(dir);
        }
        return dir != null ? Path.Combine(dir, "src", "Cyan.Design.Themes") : throw new DirectoryNotFoundException("Could not find Themes root.");
    }

    private static HashSet<string> ExtractKeys(string filePath)
    {
        var content = File.ReadAllText(filePath);
        return new HashSet<string>(KeyRegex.Matches(content).Select(m => m.Groups[1].Value));
    }

    private static List<string> ExtractColorValues(string filePath)
    {
        var content = File.ReadAllText(filePath);
        return ColorRegex.Matches(content).Select(m => m.Groups[1].Value).ToList();
    }

    private static string? ExtractColorByKey(string filePath, string key)
    {
        var content = File.ReadAllText(filePath);
        var pattern = $@"x:Key=""{Regex.Escape(key)}""\s+Color=""(#[0-9A-Fa-f]{{8}})""";
        var match = Regex.Match(content, pattern, RegexOptions.Compiled);
        return match.Success ? match.Groups[1].Value : null;
    }

    private static string[] GetAllPaletteFiles()
    {
        var root = FindThemesRoot();
        return new[]
        {
            Path.Combine(root, "AntDesign", "Light.axaml"),
            Path.Combine(root, "AntDesign", "Dark.axaml"),
            Path.Combine(root, "Shadcn", "Light.axaml"),
            Path.Combine(root, "Shadcn", "Dark.axaml"),
            Path.Combine(root, "ParkUI", "Light.axaml"),
            Path.Combine(root, "ParkUI", "Dark.axaml"),
            Path.Combine(root, "Mantine", "Light.axaml"),
            Path.Combine(root, "Mantine", "Dark.axaml")
        };
    }

    [Fact]
    public void AntDesign_Light_Dark_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var lightKeys = ExtractKeys(Path.Combine(root, "AntDesign", "Light.axaml"));
        var darkKeys = ExtractKeys(Path.Combine(root, "AntDesign", "Dark.axaml"));

        var missingInDark = lightKeys.Except(darkKeys).ToList();
        var missingInLight = darkKeys.Except(lightKeys).ToList();

        Assert.Empty(missingInDark);
        Assert.Empty(missingInLight);
    }

    [Fact]
    public void Shadcn_Light_Dark_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var lightKeys = ExtractKeys(Path.Combine(root, "Shadcn", "Light.axaml"));
        var darkKeys = ExtractKeys(Path.Combine(root, "Shadcn", "Dark.axaml"));

        var missingInDark = lightKeys.Except(darkKeys).ToList();
        var missingInLight = darkKeys.Except(lightKeys).ToList();

        Assert.Empty(missingInDark);
        Assert.Empty(missingInLight);
    }

    [Fact]
    public void ParkUI_Light_Dark_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var lightKeys = ExtractKeys(Path.Combine(root, "ParkUI", "Light.axaml"));
        var darkKeys = ExtractKeys(Path.Combine(root, "ParkUI", "Dark.axaml"));

        var missingInDark = lightKeys.Except(darkKeys).ToList();
        var missingInLight = darkKeys.Except(lightKeys).ToList();

        Assert.Empty(missingInDark);
        Assert.Empty(missingInLight);
    }

    [Fact]
    public void Mantine_Light_Dark_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var lightKeys = ExtractKeys(Path.Combine(root, "Mantine", "Light.axaml"));
        var darkKeys = ExtractKeys(Path.Combine(root, "Mantine", "Dark.axaml"));

        var missingInDark = lightKeys.Except(darkKeys).ToList();
        var missingInLight = darkKeys.Except(lightKeys).ToList();

        Assert.Empty(missingInDark);
        Assert.Empty(missingInLight);
    }

    [Fact]
    public void All_Themes_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var antKeys = ExtractKeys(Path.Combine(root, "AntDesign", "Light.axaml"));
        var shadKeys = ExtractKeys(Path.Combine(root, "Shadcn", "Light.axaml"));
        var parkKeys = ExtractKeys(Path.Combine(root, "ParkUI", "Light.axaml"));
        var mantKeys = ExtractKeys(Path.Combine(root, "Mantine", "Light.axaml"));

        var allKeys = new[] { ("AntDesign", antKeys), ("Shadcn", shadKeys), ("ParkUI", parkKeys), ("Mantine", mantKeys) };
        for (var i = 0; i < allKeys.Length; i++)
        {
            for (var j = i + 1; j < allKeys.Length; j++)
            {
                var missingInJ = allKeys[i].Item2.Except(allKeys[j].Item2).ToList();
                var missingInI = allKeys[j].Item2.Except(allKeys[i].Item2).ToList();
                Assert.Empty(missingInJ);
                Assert.Empty(missingInI);
            }
        }
    }

    [Fact]
    public void AntDesign_Shadcn_KeySet_Should_Be_Symmetric()
    {
        var root = FindThemesRoot();
        var antKeys = ExtractKeys(Path.Combine(root, "AntDesign", "Light.axaml"));
        var shadKeys = ExtractKeys(Path.Combine(root, "Shadcn", "Light.axaml"));

        var missingInShad = antKeys.Except(shadKeys).ToList();
        var missingInAnt = shadKeys.Except(antKeys).ToList();

        Assert.Empty(missingInShad);
        Assert.Empty(missingInAnt);
    }

    [Fact]
    public void All_Color_Values_Should_Use_ARGB_Format()
    {
        foreach (var file in GetAllPaletteFiles())
        {
            var colors = ExtractColorValues(file);
            foreach (var color in colors)
            {
                Assert.True(color.Length == 9 && color.StartsWith("#"),
                    $"Color value '{color}' in {Path.GetFileName(file)} is not in #AARRGGBB format.");
            }
        }
    }

    [Fact]
    public void ColorTextOnPrimaryBrush_Should_Exist_In_All_Palettes()
    {
        foreach (var file in GetAllPaletteFiles())
        {
            var keys = ExtractKeys(file);
            Assert.Contains("ColorTextOnPrimaryBrush", keys);
        }
    }

    [Fact]
    public void ColorTextOnDangerBrush_Should_Exist_In_All_Palettes()
    {
        foreach (var file in GetAllPaletteFiles())
        {
            var keys = ExtractKeys(file);
            Assert.Contains("ColorTextOnDangerBrush", keys);
        }
    }

    [Fact]
    public void ColorTextOnPrimaryBrush_Should_Differ_From_ColorPrimaryBrush()
    {
        foreach (var file in GetAllPaletteFiles())
        {
            var onPrimary = ExtractColorByKey(file, "ColorTextOnPrimaryBrush");
            var primary = ExtractColorByKey(file, "ColorPrimaryBrush");
            Assert.NotNull(onPrimary);
            Assert.NotNull(primary);
            Assert.NotEqual(onPrimary, primary);
        }
    }

    [Fact]
    public void ColorTextOnDangerBrush_Should_Differ_From_ColorErrorBrush()
    {
        foreach (var file in GetAllPaletteFiles())
        {
            var onDanger = ExtractColorByKey(file, "ColorTextOnDangerBrush");
            var error = ExtractColorByKey(file, "ColorErrorBrush");
            Assert.NotNull(onDanger);
            Assert.NotNull(error);
            Assert.NotEqual(onDanger, error);
        }
    }
}
