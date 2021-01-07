namespace GameSaveManager.DesktopApp.Components
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public partial class ButtonBaseComponent : UserControl
    {
        public ButtonBaseComponent() => InitializeComponent();

        public string Data
        {
            get => (string)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static DependencyProperty DataProperty =
           DependencyProperty.Register(nameof(Data), typeof(string), typeof(ButtonBaseComponent));

        public event RoutedEventHandler Click
        {
            add { Button.AddHandler(ButtonBase.ClickEvent, value); }
            remove { Button.RemoveHandler(ButtonBase.ClickEvent, value); }
        }
    }
}
