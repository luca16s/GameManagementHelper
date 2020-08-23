using GameSaveManager.Core.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace GameSaveManager.View.Helper
{
    public static class HelperMethods
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => string.Equals(w?.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public static string Description(this Enum value)
        {
            if (value is null)
                return null;

            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return (attributes?.First() as DescriptionAttribute)?.Description;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(str: value.ToString().Replace("_", " ", StringComparison.InvariantCultureIgnoreCase)));
        }

        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type type)
        {
            if (type is null)
                return null;

            return type.IsEnum
                ? Enum.GetValues(type).Cast<Enum>().Select((e)
                => new ValueDescription()
                {
                    Value = e,
                    Description = e.Description()
                }).ToList()
                : throw new ArgumentException($"{nameof(type)} must be an enum type");
        }
    }
}