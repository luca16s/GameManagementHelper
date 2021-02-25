namespace GameSaveManager.DesktopApp.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    using DeadFishStudio.CoreLibrary.Extensions;

    using GameSaveManager.Core.Models;

    [ValueConversion(typeof(Enum), typeof(IEnumerable<EnumModel>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Enum enumerador ? enumerador.GetAllValuesAndDescriptions() : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}