using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Display;

/// <summary>头像组控件，用于并排显示多个头像</summary>
public class CyanAvatarGroup : StackPanel
{
    /// <summary>最大显示数量样式属性</summary>
    public static readonly StyledProperty<int> MaxCountProperty =
        AvaloniaProperty.Register<CyanAvatarGroup, int>(nameof(MaxCount), 0);

    static CyanAvatarGroup()
    {
        OrientationProperty.OverrideDefaultValue<CyanAvatarGroup>(Avalonia.Layout.Orientation.Horizontal);
        SpacingProperty.OverrideDefaultValue<CyanAvatarGroup>(-8.0);
    }

    /// <summary>最大显示数量，超出部分以加号头像展示</summary>
    public int MaxCount
    {
        get => GetValue(MaxCountProperty);
        set => SetValue(MaxCountProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdateChildren();
    }

    private void UpdateChildren()
    {
        var avatars = Children.OfType<CyanAvatar>().ToList();
        int count = avatars.Count;
        int max = MaxCount;

        if (max > 0 && count > max)
        {
            int visibleCount = max - 1;
            int overflow = count - visibleCount;

            for (int i = 0; i < count; i++)
            {
                avatars[i].IsVisible = i < visibleCount;
                avatars[i].ZIndex = count - i;
            }

            var overflowAvatar = Children.OfType<CyanAvatar>()
                .FirstOrDefault(a => a.Classes.Contains("overflow"));
            if (overflowAvatar == null)
            {
                overflowAvatar = new CyanAvatar
                {
                    Classes = { "overflow" },
                    Shape = AvatarShape.Circle,
                    Background = this.TryFindResource("ColorBgElevatedBrush", out var rb) && rb is IBrush bb ? bb : Avalonia.Media.Brushes.White,
                    Foreground = this.TryFindResource("ColorBorderBrush", out var rg) && rg is IBrush bg ? bg : Avalonia.Media.Brushes.Gray,
                };
                Children.Add(overflowAvatar);
            }
            overflowAvatar.Text = $"+{overflow}";
            overflowAvatar.IsVisible = true;
            overflowAvatar.ZIndex = 0;

            var firstVisible = avatars.FirstOrDefault(a => a.IsVisible);
            if (firstVisible != null)
            {
                overflowAvatar.Size = firstVisible.Size;
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                avatars[i].IsVisible = true;
                avatars[i].ZIndex = count - i;
            }
            var overflowAvatar = Children.OfType<CyanAvatar>()
                .FirstOrDefault(a => a.Classes.Contains("overflow"));
            if (overflowAvatar != null)
            {
                overflowAvatar.IsVisible = false;
            }
        }
    }
}
