﻿namespace GameSaveManager.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    using GameSaveManager.Core.Enums;

    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            if (value is null)
                return null;

            object[] attributes = value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return (attributes?.First() as DescriptionAttribute)?.Description;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            return ti.ToTitleCase(ti.ToLower(str: value.ToString().Replace("_", " ", StringComparison.InvariantCultureIgnoreCase)));
        }

        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(this Type type)
        {
            return !(type is null) && type.IsEnum
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