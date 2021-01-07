namespace GameSaveManager.DesktopApp.Components
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public partial class ButtonBaseComponent : UserControl
    {
        public ButtonBaseComponent() => InitializeComponent();

        public object Data
        {
            get
            {
                string data = (string)GetValue(DataProperty);

                return string.IsNullOrWhiteSpace(data) ? DependencyProperty.UnsetValue : data;
            }

            set => SetValue(DataProperty, value);
        }

        private static readonly DependencyProperty DataProperty =
           DependencyProperty.Register(
               nameof(Data),
               typeof(object),
               typeof(ButtonBaseComponent));

        public ICommand ImageButtonCommand
        {
            get => (ICommand)GetValue(Command);
            set => SetValue(Command, value);
        }

        private static readonly DependencyProperty Command =
            DependencyProperty.Register(
                nameof(ImageButtonCommand),
                typeof(ICommand),
                typeof(ButtonBaseComponent));

        public event RoutedEventHandler Click
        {
            add { Button.AddHandler(ButtonBase.ClickEvent, value); }
            remove { Button.RemoveHandler(ButtonBase.ClickEvent, value); }
        }
    }
}
