using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class InputNumberTests
{
    [AvaloniaFact]
    public void CyanInputNumber_Default_Properties_Should_Be_Expected()
    {
        var input = new CyanInputNumber();
        Assert.Null(input.Value);
        Assert.Null(input.Min);
        Assert.Null(input.Max);
        Assert.Equal(1, input.Step);
        Assert.Equal(-1, input.Precision);
        Assert.True(input.ShowControls);
        Assert.True(input.KeyboardNavigation);
        Assert.True(input.ChangeOnBlur);
        Assert.False(input.ChangeOnWheel);
        Assert.Equal(ControlSize.Middle, input.InputSize);
        Assert.Equal(InputStatus.Default, input.InputStatus);
        Assert.Equal(InputVariant.Outlined, input.InputVariant);
        Assert.Equal(HandleVisible.Always, input.HandleVisible);
        Assert.Equal(NumberControlMode.Vertical, input.ControlMode);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Increment_Should_Increase_Value_By_Step()
    {
        var input = new CyanInputNumber { Value = 5, Step = 2 };
        input.Increment();
        Assert.Equal(7, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Decrement_Should_Decrease_Value_By_Step()
    {
        var input = new CyanInputNumber { Value = 5, Step = 2 };
        input.Decrement();
        Assert.Equal(3, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Increment_From_Null_Should_Start_From_Min_Or_Zero()
    {
        var input = new CyanInputNumber { Step = 1 };
        input.Increment();
        Assert.Equal(1, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Increment_From_Null_With_Min_Should_Start_From_Min()
    {
        var input = new CyanInputNumber { Min = 10, Step = 1 };
        input.Increment();
        Assert.Equal(11, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Should_Clamp_To_Max()
    {
        var input = new CyanInputNumber { Value = 9, Max = 10, Step = 5 };
        input.Increment();
        Assert.Equal(10, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Should_Clamp_To_Min()
    {
        var input = new CyanInputNumber { Value = 1, Min = 0, Step = 5 };
        input.Decrement();
        Assert.Equal(0, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Precision_Should_Round_Value()
    {
        var input = new CyanInputNumber { Value = 0, Step = 1, Precision = 2 };
        input.Increment();
        input.Increment();
        Assert.Equal(2, input.Value);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Formatter_Should_Receive_New_Value_Not_Old()
    {
        double? formattedArg = null;
        var input = new CyanInputNumber { Value = 5, Step = 1 };
        input.Formatter = val =>
        {
            formattedArg = val;
            return val?.ToString("F2") ?? "";
        };
        input.Increment();
        Assert.Equal(6, formattedArg);
    }

    [AvaloniaFact]
    public void CyanInputNumber_ValueChanged_Should_Fire_On_Step()
    {
        var input = new CyanInputNumber { Value = 5, Step = 1 };
        var fired = false;
        input.ValueChanged += (_, _) => fired = true;
        input.Increment();
        Assert.True(fired);
    }

    [AvaloniaFact]
    public void CyanInputNumber_Stepped_Should_Fire_With_Correct_Offset()
    {
        var input = new CyanInputNumber { Value = 5, Step = 3 };
        StepEventArgs? args = null;
        input.Stepped += (_, e) => args = e;
        input.Increment();
        Assert.NotNull(args);
        Assert.Equal(3, args!.Offset);
        Assert.Equal(StepType.Up, args.Type);
    }
}