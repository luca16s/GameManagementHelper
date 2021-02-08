namespace GameSaveManager.Core.Utils
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class StringExtensions
    {
        public static T GetEnumValueFromDescription<T>(this string description) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(SystemMessages.ErrorItemNotFoundOnEnum, nameof(description));
        }
    }
}