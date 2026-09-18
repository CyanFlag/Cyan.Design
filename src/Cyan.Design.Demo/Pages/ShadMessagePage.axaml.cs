using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Chat;

namespace Cyan.Design.Demo.Pages;

public partial class ShadMessagePage : UserControl
{
    private StackPanel? _chatList;
    private ScrollViewer? _chatScroll;
    private TextBox? _input;

    private static readonly string[] s_replies =
    [
        "好的，我收到了。",
        "让我想想...",
        "这个问题很有意思！",
        "你可以试试另一种方法。",
        "明白了，我马上处理。",
        "Thanks for the update!",
        "Got it. On it right away.",
    ];

    private static readonly IBrush s_primary = new SolidColorBrush(Color.Parse("#2563EB"));
    private static readonly IBrush s_fill = new SolidColorBrush(Color.Parse("#F3F4F6"));
    private static readonly IBrush s_white = Brushes.White;
    private static readonly IBrush s_black = Brushes.Black;

    public ShadMessagePage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _chatList = this.FindControl<StackPanel>("PART_ChatList");
        _chatScroll = this.FindControl<ScrollViewer>("PART_ChatScroll");
        _input = this.FindControl<TextBox>("PART_Input");

        if (_chatList is not null && _chatList.Children.Count == 0)
        {
            AddMessage("你好！有什么可以帮你的吗？", false);
        }
    }

    private void OnSendClick(object? sender, RoutedEventArgs e) => SendMessage();

    private void OnInputKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            SendMessage();
    }

    private void SendMessage()
    {
        _chatList ??= this.FindControl<StackPanel>("PART_ChatList");
        _chatScroll ??= this.FindControl<ScrollViewer>("PART_ChatScroll");
        _input ??= this.FindControl<TextBox>("PART_Input");

        if (_input is null || _chatList is null) return;
        var text = _input.Text?.Trim();
        if (string.IsNullOrEmpty(text)) return;

        AddMessage(text, true);
        _input.Text = string.Empty;
        ScrollToBottom();

        _ = Task.Delay(600).ContinueWith(_ =>
        {
            var reply = s_replies[Random.Shared.Next(s_replies.Length)];
            Dispatcher.UIThread.Post(() =>
            {
                AddMessage(reply, false);
                ScrollToBottom();
            });
        });
    }

    private void AddMessage(string text, bool isMe)
    {
        if (_chatList is null) return;

        var avatar = new Border
        {
            Width = 32,
            Height = 32,
            CornerRadius = new CornerRadius(16),
            Background = isMe ? s_primary : s_fill,
            VerticalAlignment = VerticalAlignment.Bottom,
            Child = new TextBlock
            {
                Text = isMe ? "ME" : "AI",
                FontSize = isMe ? 12 : 14,
                FontWeight = FontWeight.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = isMe ? s_white : s_black,
            },
        };

        var bubble = new Border
        {
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(12, 8),
            MaxWidth = 400,
            Background = isMe ? s_primary : s_fill,
            Child = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = isMe ? s_white : s_black,
            },
        };

        var msg = new CyanShadMessage
        {
            Align = isMe ? ShadMessageAlign.End : ShadMessageAlign.Start,
        };

        if (isMe)
        {
            msg.Children.Add(bubble);
            msg.Children.Add(avatar);
        }
        else
        {
            msg.Children.Add(avatar);
            msg.Children.Add(bubble);
        }

        _chatList.Children.Add(msg);
    }

    private void ScrollToBottom()
    {
        if (_chatScroll is not null)
            _chatScroll.Offset = new Vector(0, double.MaxValue);
    }
}
