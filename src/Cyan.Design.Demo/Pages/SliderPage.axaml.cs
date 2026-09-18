using Avalonia.Controls;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class SliderPage : UserControl
{
    public SliderPage()
    {
        InitializeComponent();
        BasicSlider.ValueChanged += (_, _) => BasicValue.Text = BasicSlider.Value.ToString("F0");
        StepSlider.ValueChanged += (_, _) => StepValue.Text = StepSlider.Value.ToString("F0");
        ReverseSlider.ValueChanged += (_, _) => ReverseValue.Text = ReverseSlider.Value.ToString("F0");
        RangeSlider.ValueChanged += (_, _) => RangeValue.Text = RangeSlider.Value.ToString("F0");
        DotSlider.ValueChanged += (_, _) => DotValue.Text = DotSlider.Value.ToString("F0");
        DualSlider.RangeChanged += (_, _) => DualValue.Text = $"{DualSlider.RangeStart:F0} - {DualSlider.RangeEnd:F0}";
        DualStepSlider.RangeChanged += (_, _) => DualStepValue.Text = $"{DualStepSlider.RangeStart:F0} - {DualStepSlider.RangeEnd:F0}";

    }
}
