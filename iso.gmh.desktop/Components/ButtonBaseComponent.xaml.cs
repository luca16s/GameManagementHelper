namespace iso.gmh.desktop.Components;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public partial class ButtonBaseComponent : UserControl
{
    public ButtonBaseComponent() => InitializeComponent();

    public object Image
    {
        get
        {
            string data = GetValue(ImageProperty).ToString();

            return string.IsNullOrWhiteSpace(data)
                ? DependencyProperty.UnsetValue
                : data;
        }

        set => SetValue(ImageProperty, value);
    }

    private static readonly DependencyProperty ImageProperty =
       DependencyProperty.Register(
           nameof(Image),
           typeof(object),
           typeof(ButtonBaseComponent));

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(Command);
        set => SetValue(Command, value);
    }

    private static readonly DependencyProperty Command =
        DependencyProperty.Register(
            nameof(ButtonCommand),
            typeof(ICommand),
            typeof(ButtonBaseComponent));

    public object ButtonCommandParameters
    {
        get => (object)GetValue(CommandParameters);
        set => SetValue(CommandParameters, value);
    }

    private static readonly DependencyProperty CommandParameters =
        DependencyProperty.Register(
            nameof(ButtonCommandParameters),
            typeof(object),
            typeof(ButtonBaseComponent));

    public event RoutedEventHandler Click
    {
        add { Button.Click += value; }
        remove { Button.Click += value; }
    }
}