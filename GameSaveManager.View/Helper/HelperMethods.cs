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
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(str: value.ToString().Replace("_", " ", StringComparison.InvariantCultureIgnoreCase)));
        }

        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type t)
        {
            if (t is null)
                return null;

            return t.IsEnum
                ? Enum.GetValues(t).Cast<Enum>().Select((e) => new ValueDescription() { Value = e, Description = e.Description() }).ToList()
                : throw new ArgumentException($"{nameof(t)} must be an enum type");
        }
    }
}