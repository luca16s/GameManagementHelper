namespace iso.gmh.desktop.Helper
{
    using System;
    using System.Linq;
    using System.Windows;

    public static class HelperMethods
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => string.Equals(w?.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}