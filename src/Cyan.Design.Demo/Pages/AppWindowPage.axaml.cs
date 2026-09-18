using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Windowing;

namespace Cyan.Design.Demo.Pages;

public partial class AppWindowPage : UserControl
{
    public AppWindowPage()
    {
        InitializeComponent();
    }

    private static CyanAppWindow CreateWindow(string title, double width = 600, double height = 400)
    {
        return new CyanAppWindow
        {
            Title = title,
            Width = width,
            Height = height,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
    }

    private static IBrush? GetBrush(string key)
        => Application.Current?.TryFindResource(key, out var v) == true && v is IBrush b ? b : null;

    private static void ShowWindow(CyanAppWindow window)
    {
        window.Content = new TextBlock
        {
            Text = $"这是 {window.Title} 的内容区域。",
            FontSize = 16,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Foreground = GetBrush("ColorTextBrush")
        };
        window.Show();
    }

    private void OnOpenBasic(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("基本 AppWindow");
        ShowWindow(window);
    }

    private void OnOpenExtended(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("内容延伸到标题栏");
        window.TitleBar.ExtendsContentIntoTitleBar = true;

        var panel = new StackPanel { Margin = new Thickness(16) };
        panel.Children.Add(new TextBlock
        {
            Text = "内容延伸到标题栏区域",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = GetBrush("ColorTextBrush")
        });
        panel.Children.Add(new TextBlock
        {
            Text = "ExtendsContentIntoTitleBar = true",
            Margin = new Thickness(0, 8, 0, 0),
            Foreground = GetBrush("ColorTextSecondaryBrush")
        });
        window.Content = panel;
        window.Show();
    }

    private void OnOpenCustomColors(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("自定义标题栏颜色");
        window.TitleBar.BackgroundColor = Color.Parse("#FF1F1F1F");
        window.TitleBar.ForegroundColor = Color.Parse("#FFFFFFFF");
        window.TitleBar.InactiveBackgroundColor = Color.Parse("#FF2D2D2D");
        window.TitleBar.InactiveForegroundColor = Color.Parse("#FF8C8C8C");
        window.TitleBar.ButtonHoverBackgroundColor = Color.Parse("#FF3D3D3D");
        window.TitleBar.ButtonHoverForegroundColor = Color.Parse("#FFFFFFFF");
        ShowWindow(window);
    }

    private void OnOpenNoFullScreen(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("隐藏全屏按钮");
        window.TitleBar.ShowFullScreenButton = false;
        ShowWindow(window);
    }

    private void OnOpenAsDialog(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("对话框模式");
        window.ShowAsDialog = true;
        ShowWindow(window);
    }

    private void OnOpenSmallTitleBar(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("小标题栏 (24px)");
        window.TitleBar.Height = 24;
        ShowWindow(window);
    }

    private void OnOpenLargeTitleBar(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("大标题栏 (48px)");
        window.TitleBar.Height = 48;
        ShowWindow(window);
    }

    private void OnOpenMica(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("Mica 背景效果", 700, 500);
        window.Backdrop = WindowBackdrop.Mica;
        ShowWindow(window);
    }

    private void OnOpenAcrylic(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("Acrylic 背景效果", 700, 500);
        window.Backdrop = WindowBackdrop.Acrylic;
        ShowWindow(window);
    }

    private void OnOpenTabbed(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("Tabbed 背景效果", 700, 500);
        window.Backdrop = WindowBackdrop.Tabbed;
        ShowWindow(window);
    }

    private void OnOpenCustomTitleBarContent(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("自定义标题栏内容", 700, 450);
        window.TitleBar.ExtendsContentIntoTitleBar = true;

        var titleBarPanel = new StackPanel
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Margin = new Thickness(12, 0, 0, 0),
            Spacing = 8
        };
        titleBarPanel.Children.Add(new TextBlock
        {
            Text = "🔍",
            FontSize = 14,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        titleBarPanel.Children.Add(new TextBlock
        {
            Text = "自定义标题栏内容",
            FontSize = 13,
            FontWeight = FontWeight.Bold,
            Foreground = GetBrush("ColorTextBrush"),
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        titleBarPanel.Children.Add(new TextBlock
        {
            Text = "— TitleBarContent",
            FontSize = 12,
            Foreground = GetBrush("ColorTextSecondaryBrush"),
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        window.TitleBarContent = titleBarPanel;

        var content = new StackPanel { Margin = new Thickness(16), Spacing = 12 };
        content.Children.Add(new TextBlock
        {
            Text = "标题栏区域放置了自定义内容",
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = GetBrush("ColorTextBrush")
        });
        content.Children.Add(new TextBlock
        {
            Text = "使用 TitleBarContent 属性 + ExtendsContentIntoTitleBar = true，可在标题栏区域放置任意控件。",
            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
            Foreground = GetBrush("ColorTextSecondaryBrush")
        });
        window.Content = content;
        window.Show();
    }

    private void OnOpenRounded(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("圆角窗口 (8px)", 600, 400);
        window.CornerRadius = new CornerRadius(8);
        ShowWindow(window);
    }

    private void OnOpenLargeRounded(object? sender, RoutedEventArgs e)
    {
        var window = CreateWindow("圆角窗口 (16px)", 600, 400);
        window.CornerRadius = new CornerRadius(16);
        ShowWindow(window);
    }

    private void OnOpenNoTitleBar(object? sender, RoutedEventArgs e)
    {
        var bgBrush = GetBrush("ColorBgLayoutBrush") ?? Brushes.White;

        var window = new Window
        {
            Title = "无标题栏窗口",
            Width = 600,
            Height = 400,
            WindowDecorations = WindowDecorations.BorderOnly,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = bgBrush
        };

        var captionBrush = GetBrush("ColorTextBrush") ?? Brushes.Black;

        Button MakeCaptionButton(string iconData)
        {
            var btn = new Button
            {
                Width = 45,
                Height = 32,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                Foreground = captionBrush,
                Content = new Viewbox
                {
                    Width = 11,
                    Margin = new Thickness(2),
                    Child = new Avalonia.Controls.Shapes.Path
                    {
                        Data = PathGeometry.Parse(iconData),
                        Stretch = Stretch.UniformToFill,
                        Fill = captionBrush
                    }
                }
            };
            return btn;
        }

        var minBtn = MakeCaptionButton("M2048 1229v-205h-2048v205h2048z");
        minBtn.Click += (_, _) => window.WindowState = WindowState.Minimized;

        var maxBtn = MakeCaptionButton("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
        maxBtn.Click += (_, _) => window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        var closeBtn = MakeCaptionButton("M1169 1024l879 -879l-145 -145l-879 879l-879 -879l-145 145l879 879l-879 879l145 145l879 -879l879 879l145 -145z");
        closeBtn.Click += (_, _) => window.Close();

        var captionPanel = new StackPanel
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top,
            Spacing = 2
        };
        captionPanel.Children.Add(minBtn);
        captionPanel.Children.Add(maxBtn);
        captionPanel.Children.Add(closeBtn);

        var contentPanel = new StackPanel
        {
            Margin = new Thickness(16, 40, 16, 16),
            Spacing = 12
        };
        contentPanel.Children.Add(new TextBlock
        {
            Text = "无标题栏窗口（带边框）",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = GetBrush("ColorTextBrush")
        });
        contentPanel.Children.Add(new TextBlock
        {
            Text = "SystemDecorations = BorderOnly，有边框无系统标题栏，右上角为自定义按钮。",
            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
            Foreground = GetBrush("ColorTextSecondaryBrush")
        });

        var dragBar = new Panel
        {
            Height = 32,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            Background = Brushes.Transparent
        };
        dragBar.PointerPressed += (_, e) =>
        {
            if (e.Source is Button) return;
            window.BeginMoveDrag(e);
        };

        var grid = new Grid { Background = bgBrush };
        grid.Children.Add(dragBar);
        grid.Children.Add(contentPanel);
        grid.Children.Add(captionPanel);


        window.Content = grid;
        window.Show();
    }
}