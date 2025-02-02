using Avalonia;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace PCL2.Neo.Controls.Attachments;

public sealed class PathIcon : AvaloniaObject {
    public static readonly AttachedProperty<ITransform?> TransformProperty = AvaloniaProperty.RegisterAttached<PathIcon, Interactive, ITransform?>(
        "Transform", default, false, BindingMode.OneTime);

    public static void SetTransform(AvaloniaObject element, ITransform? value) {
        element.SetValue(TransformProperty, value);
    }

    public static ITransform? GetTransform(AvaloniaObject element) {
        return element.GetValue(TransformProperty);
    }
}