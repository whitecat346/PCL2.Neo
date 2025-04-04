using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Layout;
using Avalonia.Styling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PCL2.Neo.Animations
{
    public class YAnimation(Animatable control, TimeSpan duration, TimeSpan delay, double value, Easing easing)
        : IAnimation
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        public Animatable Control { get; set; } = control;
        public TimeSpan Duration { get; set; } = duration;
        public TimeSpan Delay { get; set; } = delay;
        public double Value { get; set; } = value;
        public Easing Easing { get; set; } = easing;
        public bool Wait { get; set; } = false;

        public YAnimation(Animatable control, double value) : this(
            control, value, new LinearEasing())
        {
        }
        public YAnimation(Animatable control, double value, Easing easing) : this(
            control, TimeSpan.FromSeconds(1), value, easing)
        {
        }
        public YAnimation(Animatable control, TimeSpan duration, double value) : this(
            control, duration, value, new LinearEasing())
        {
        }
        public YAnimation(Animatable control, TimeSpan duration, TimeSpan delay, double value) : this(
            control, duration, delay, value, new LinearEasing())
        {
        }
        public YAnimation(Animatable control, TimeSpan duration, double value, Easing easing) : this(
            control, duration, TimeSpan.Zero, value, easing)
        {
        }

        public async Task RunAsync()
        {
            var control = (Layoutable)Control;
            Thickness marginOriginal = control.Margin;
            Thickness margin = control.VerticalAlignment switch
            {
                VerticalAlignment.Top => new Thickness(control.Margin.Left, control.Margin.Top + Value,
                    control.Margin.Right, control.Margin.Bottom),
                VerticalAlignment.Bottom => new Thickness(control.Margin.Left, control.Margin.Top, control.Margin.Right,
                    control.Margin.Bottom - Value),
                _ => control.Margin
            };
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
                            new Setter(Layoutable.MarginProperty, marginOriginal)
                        },
                        Cue = new Cue(1d)
                    },
                    new KeyFrame
                    {
                        Setters =
                        {
                            new Setter(Layoutable.MarginProperty, margin)
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