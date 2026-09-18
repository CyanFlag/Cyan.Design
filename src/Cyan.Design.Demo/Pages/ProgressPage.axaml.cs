using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class ProgressPage : UserControl
{
    private DispatcherTimer? _timer;
    private bool _increasing = true;

    public ProgressPage()
    {
        InitializeComponent();
        StepCountSlider.ValueChanged += (_, e) =>
        {
            var count = (int)e.NewValue;
            CustomStepsProgress.Steps = count;
            CircleSteps1.Steps = count;
            CircleSteps2.Steps = count;
        };
        StepGapSlider.ValueChanged += (_, e) =>
        {
            CustomStepsProgress.Gap = e.NewValue;
            CircleSteps1.Gap = e.NewValue;
            CircleSteps2.Gap = e.NewValue;
        };
        PosPercentSlider.ValueChanged += (_, e) => InteractivePosProgress.Percent = e.NewValue;
        PosRight.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Right;
        PosTop.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Top;
        PosBottom.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Bottom;
        PosInside.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Inside;
        PosCenter.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Center;
        PosFollow.Click += (_, _) => InteractivePosProgress.InfoPosition = ProgressInfoPosition.Follow;
    }

    private void OnToggleDynamic(object? sender, RoutedEventArgs e)
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer = null;
            return;
        }

        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, OnTimerTick);
        _timer.Start();
    }

    private void OnTimerTick(object? sender, System.EventArgs e)
    {
        var p = DynamicProgress.Percent;
        if (_increasing)
        {
            p += 2;
            if (p >= 100)
            {
                p = 100;
                _increasing = false;
            }
        }
        else
        {
            p -= 2;
            if (p <= 0)
            {
                p = 0;
                _increasing = true;
            }
        }
        DynamicProgress.Percent = p;
    }
}
