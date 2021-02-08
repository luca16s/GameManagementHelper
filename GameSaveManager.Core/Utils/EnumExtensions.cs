namespace GameSaveManager.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    using GameSaveManager.Core.Models;

    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            object[] attributes = value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return (attributes?.First() as DescriptionAttribute)?.Description;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            return ti
                .ToTitleCase(ti
                .ToLower(str: value.ToString().Replace("_", " ", StringComparison.InvariantCultureIgnoreCase)));
        }

        public static IEnumerable<EnumModel> GetAllValuesAndDescriptions(this Type type)
        {
            return type.IsEnum
                ? Enum.GetValues(type).Cast<Enum>().Select((e)
                => new EnumModel()
                {
                    Value = e,
                    Description = e.Description()
                }).ToList()
                : throw new ArgumentException(string.Format(SystemMessages.MustBeAnEnumTypeMessage, type.Name));
        }
    }
}