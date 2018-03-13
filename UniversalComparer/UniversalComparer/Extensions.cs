using System.Linq;

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
            var properties = value?.GetType().GetProperties();

            foreach (var propertyName in sortFieldProperties)
            {
                var property = properties?.FirstOrDefault(p => p.Name == propertyName);
                value = property?.GetValue(value, null);
                properties = value?.GetType().GetProperties();
            }

            return value;
        }
    }
}
