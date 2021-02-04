namespace GameSaveManager.DesktopApp.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    [ValueConversion(typeof(Enum), typeof(IEnumerable<EnumModel>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.GetType().GetAllValuesAndDescriptions();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}