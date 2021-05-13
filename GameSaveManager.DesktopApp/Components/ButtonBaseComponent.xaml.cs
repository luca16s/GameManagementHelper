namespace GameSaveManager.DesktopApp.Components
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class ButtonBaseComponent : UserControl
    {
        private static readonly DependencyProperty Command =
            DependencyProperty.Register(
                nameof(ButtonCommand),
                typeof(ICommand),
                typeof(ButtonBaseComponent));

        private static readonly DependencyProperty CommandParameters =
            DependencyProperty.Register(
                nameof(ButtonCommandParameters),
                typeof(object),
                typeof(ButtonBaseComponent));

        private static readonly DependencyProperty ImageProperty =
           DependencyProperty.Register(
               nameof(Image),
               typeof(object),
               typeof(ButtonBaseComponent));

        public ButtonBaseComponent() => InitializeComponent();

        public event RoutedEventHandler Click
        {
            add { Button.Click += value; }
            remove { Button.Click += value; }
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(Command);
            set => SetValue(Command, value);
        }

        public object ButtonCommandParameters
        {
            get => GetValue(CommandParameters);
            set => SetValue(CommandParameters, value);
        }

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
    }
}