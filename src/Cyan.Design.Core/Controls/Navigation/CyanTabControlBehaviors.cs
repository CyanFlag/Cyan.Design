using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>标签控件滑动胶囊选中效果附加行为</summary>
public static class CyanTabControlBehaviors
{
    /// <summary>是否启用滑动胶囊附加属性</summary>
    public static readonly AttachedProperty<bool> UseSlidingPillProperty =
        AvaloniaProperty.RegisterAttached<TabControl, bool>(
            "UseSlidingPill", typeof(CyanTabControlBehaviors));

    /// <summary>胶囊内边距附加属性</summary>
    public static readonly AttachedProperty<Thickness> PillInsetProperty =
        AvaloniaProperty.RegisterAttached<TabControl, Thickness>(
            "PillInset", typeof(CyanTabControlBehaviors), new Thickness(2, 0, 2, 4));

    /// <summary>胶囊高度附加属性</summary>
    public static readonly AttachedProperty<double> PillHeightProperty =
        AvaloniaProperty.RegisterAttached<TabControl, double>(
            "PillHeight", typeof(CyanTabControlBehaviors), double.NaN);

    private static readonly TimeSpan StretchHold = TimeSpan.FromMilliseconds(140);

    private static readonly ConditionalWeakTable<TabControl, PillState> States = new();

    static CyanTabControlBehaviors()
    {
        UseSlidingPillProperty.Changed.AddClassHandler<TabControl>(OnUseSlidingPillChanged);
    }

    /// <summary>获取是否启用滑动胶囊</summary>
    public static bool GetUseSlidingPill(TabControl element) => element.GetValue(UseSlidingPillProperty);

    /// <summary>设置是否启用滑动胶囊</summary>
    public static void SetUseSlidingPill(TabControl element, bool value) =>
        element.SetValue(UseSlidingPillProperty, value);

    /// <summary>获取胶囊内边距</summary>
    public static Thickness GetPillInset(TabControl element) => element.GetValue(PillInsetProperty);

    /// <summary>设置胶囊内边距</summary>
    public static void SetPillInset(TabControl element, Thickness value) =>
        element.SetValue(PillInsetProperty, value);

    /// <summary>获取胶囊高度</summary>
    public static double GetPillHeight(TabControl element) => element.GetValue(PillHeightProperty);

    /// <summary>设置胶囊高度</summary>
    public static void SetPillHeight(TabControl element, double value) =>
        element.SetValue(PillHeightProperty, value);

    private static void OnUseSlidingPillChanged(TabControl tc, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
            Attach(tc);
        else
            Detach(tc);
    }

    private static void Attach(TabControl tc)
    {
        if (States.TryGetValue(tc, out _)) return;
        States.Add(tc, new PillState());
        tc.TemplateApplied += OnTemplateApplied;
        tc.SelectionChanged += OnSelectionChanged;
        tc.LayoutUpdated += OnLayoutUpdated;
    }

    private static void Detach(TabControl tc)
    {
        tc.TemplateApplied -= OnTemplateApplied;
        tc.SelectionChanged -= OnSelectionChanged;
        tc.LayoutUpdated -= OnLayoutUpdated;
        States.Remove(tc);
    }

    private static void OnTemplateApplied(object? sender, TemplateAppliedEventArgs e)
    {
        if (sender is not TabControl tc || !States.TryGetValue(tc, out var state)) return;

        state.Pill = e.NameScope.Find<Border>("PART_SelectionPill");
        state.ItemsPresenter = e.NameScope.Find<ItemsPresenter>("PART_ItemsPresenter");
        if (state.Pill is null) return;

        state.SavedTransitions = state.Pill.Transitions;
        state.Pill.Transitions = null;
        state.HasInitialPosition = false;

        Dispatcher.UIThread.Post(() => UpdatePill(tc, animate: false), DispatcherPriority.Loaded);
    }

    private static void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is TabControl tc)
            Dispatcher.UIThread.Post(() => UpdatePill(tc, animate: true), DispatcherPriority.Loaded);
    }

    private static void OnLayoutUpdated(object? sender, EventArgs e)
    {
        if (sender is not TabControl tc || !States.TryGetValue(tc, out var state)) return;
        if (state.Pill is null || state.ItemsPresenter is null) return;

        var rect = MeasureSelected(tc, state);
        if (rect is null) return;
        if (rect.Value == state.LastTarget && state.HasInitialPosition) return;

        UpdatePill(tc, animate: false);
    }

    private static void UpdatePill(TabControl tc, bool animate)
    {
        if (!States.TryGetValue(tc, out var state)) return;
        if (state.Pill is null || state.ItemsPresenter is null) return;

        var rect = MeasureSelected(tc, state);
        if (rect is null)
        {
            state.Pill.Opacity = 0;
            state.HasInitialPosition = false;
            return;
        }

        var target = rect.Value;

        if (!state.HasInitialPosition)
        {
            OpenPill(state, target);
        }
        else if (animate)
        {
            StretchAndSnap(state, target);
        }
        else
        {
            SetBounds(state.Pill, target.X, target.Y, target.Width, target.Height);
            state.Pill.Opacity = 1;
        }

        state.LastTarget = target;
        state.HasInitialPosition = true;
    }

    private static void OpenPill(PillState state, Rect target)
    {
        if (state.Pill is null) return;

        state.Pill.Transitions = null;
        var midX = target.X + target.Width * 0.5;
        SetBounds(state.Pill, midX, target.Y, 0, target.Height);
        state.Pill.Opacity = 0;

        var pill = state.Pill;
        var saved = state.SavedTransitions;

        Dispatcher.UIThread.Post(
            () =>
            {
                pill.Transitions = saved;
                Dispatcher.UIThread.Post(
                    () =>
                    {
                        SetBounds(pill, target.X, target.Y, target.Width, target.Height);
                        pill.Opacity = 1;
                    },
                    DispatcherPriority.Background);
            },
            DispatcherPriority.Background);
    }

    private static void StretchAndSnap(PillState state, Rect target)
    {
        if (state.Pill is null) return;

        var spanX = Math.Min(state.LastTarget.X, target.X);
        var spanRight = Math.Max(state.LastTarget.Right, target.Right);
        SetBounds(state.Pill, spanX, target.Y, spanRight - spanX, target.Height);
        state.Pill.Opacity = 1;

        var pill = state.Pill;
        DispatcherTimer.RunOnce(
            () => SetBounds(pill, target.X, target.Y, target.Width, target.Height),
            StretchHold);
    }

    private static Rect? MeasureSelected(TabControl tc, PillState state)
    {
        if (tc.SelectedIndex < 0 || state.ItemsPresenter is null) return null;
        var container = tc.ContainerFromIndex(tc.SelectedIndex);
        if (container is null || !container.IsVisible || container.Bounds.Width <= 0) return null;

        var topLeft = container.TranslatePoint(default, state.ItemsPresenter);
        if (topLeft is null) return null;

        var inset = GetPillInset(tc);
        var fixedHeight = GetPillHeight(tc);

        var x = topLeft.Value.X + inset.Left;
        var w = Math.Max(0, container.Bounds.Width - inset.Left - inset.Right);
        var innerY = topLeft.Value.Y + inset.Top;
        var innerH = Math.Max(0, container.Bounds.Height - inset.Top - inset.Bottom);

        double y, h;
        if (double.IsNaN(fixedHeight))
        {
            y = innerY;
            h = innerH;
        }
        else
        {
            h = fixedHeight;
            y = innerY + Math.Max(0, innerH - h);
        }

        return new Rect(x, y, w, h);
    }

    private static void SetBounds(Border pill, double x, double y, double w, double h)
    {
        Canvas.SetLeft(pill, x);
        Canvas.SetTop(pill, y);
        pill.Width = w;
        pill.Height = h;
    }

    private sealed class PillState
    {
        public Border? Pill;
        public ItemsPresenter? ItemsPresenter;
        public Transitions? SavedTransitions;
        public Rect LastTarget;
        public bool HasInitialPosition;
    }
}
