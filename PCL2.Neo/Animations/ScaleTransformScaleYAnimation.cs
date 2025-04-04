using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PCL2.Neo.Animations
{
    public class ScaleTransformScaleYAnimation(
        Animatable control,
        TimeSpan duration,
        TimeSpan delay,
        double? valueBefore,
        double valueAfter,
        Easing easing)
        : IAnimation
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        public Animatable Control { get; set; } = control;
        public TimeSpan Duration { get; set; } = duration;
        public TimeSpan Delay { get; set; } = delay;
        public double? ValueBefore { get; set; } = valueBefore;
        public double ValueAfter { get; set; } = valueAfter;
        public Easing Easing { get; set; } = easing;
        public bool Wait { get; set; } = false;

        public ScaleTransformScaleYAnimation(Animatable control, double valueAfter) : this(
            control, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, double valueAfter, Easing easing) : this(
            control, TimeSpan.FromSeconds(1), valueAfter, easing)
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, double valueAfter) : this(
            control, duration, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, TimeSpan delay, double valueAfter) : this(
            control, duration, delay, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, double valueAfter, Easing easing) : this(
            control, duration, control.GetValue(ScaleTransform.ScaleYProperty), valueAfter, easing)
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, TimeSpan delay, double valueAfter, Easing easing) : this(
            control, duration, delay, control.GetValue(ScaleTransform.ScaleYProperty), valueAfter, easing)
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, double? valueBefore, double valueAfter) : this(
            control, valueBefore, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, double? valueBefore, double valueAfter, Easing easing) : this(
            control, TimeSpan.FromSeconds(1), valueBefore, valueAfter, easing)
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, double? valueBefore, double valueAfter) : this(
            control, duration, valueBefore, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, TimeSpan delay, double? valueBefore, double valueAfter) : this(
            control, duration, delay, valueBefore, valueAfter, new LinearEasing())
        {
        }
        public ScaleTransformScaleYAnimation(Animatable control, TimeSpan duration, double? valueBefore, double valueAfter, Easing easing) : this(
            control, duration, TimeSpan.Zero, valueBefore, valueAfter, easing)
        {
        }

        public async Task RunAsync()
        {
            var animation = new Animation
            {
                Easing = Easing,
                Duration = Duration,
                Delay = Delay,
                FillMode = FillMode.Both,
                Children =
                {
                    new KeyFrame
                    {
                        Setters =
                        {
                            new Setter(ScaleTransform.ScaleYProperty, ValueBefore)
                        },
                        Cue = new Cue(0d)
                    },
                    new KeyFrame
                    {
                        Setters =
                        {
                            new Setter(ScaleTransform.ScaleYProperty, ValueAfter)
                        },
                        Cue = new Cue(1d)
                    }
                }
            };
            await animation.RunAsync(Control, _cancellationTokenSource.Token);
        }

        public void Cancel() => _cancellationTokenSource.Cancel();
    }
}