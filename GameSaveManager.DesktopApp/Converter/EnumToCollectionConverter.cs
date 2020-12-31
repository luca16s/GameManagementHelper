namespace GameSaveManager.DesktopApp.Converter
{
    using GameSaveManager.Core.Enums;
    using GameSaveManager.DesktopApp.Helper;

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? null : HelperMethods.GetAllValuesAndDescriptions(value.GetType());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}