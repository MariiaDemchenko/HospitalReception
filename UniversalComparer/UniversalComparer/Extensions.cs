using System;

namespace UniversalComparer
{
    static class Extensions
    {
        /// <summary>
        /// Run all the properties of the sort field, 
        /// get the object corresponding to the sort field
        /// </summary>
        /// <param name="value">Base object</param>
        /// <param name="sortFieldProperties">Sort properties splitted in sort field string with '.'</param>
        /// <returns>Object corresponding to the sort field</returns>
        public static object GetObjectByProperties(this object value, string[] sortFieldProperties)
        {
            foreach (var propertyName in sortFieldProperties)
            {
                var property = value?.GetType().GetProperty(propertyName);
                value = property?.GetValue(value, null);
            }
            return value;
        }

        /// <summary>
        /// Run all the properties of the sort field, 
        /// check if properties exist
        /// </summary>
        /// <param name="type">Base type</param>
        /// <param name="sortFieldProperties">Sort properties splitted in sort field string with '.'</param>
        /// <returns>Check result</returns>
        public static bool CheckPropertiesExist(this Type type, string[] sortFieldProperties)
        {
            foreach (var propertyName in sortFieldProperties)
            {
                var property = type?.GetProperty(propertyName);
                type = property?.PropertyType;
            }
            return type != null;
        }

        public static bool IsComparable(this object value)
        {
            return value == null || value is IComparable;
        }
    }
}