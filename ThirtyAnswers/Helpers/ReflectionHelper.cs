using System;
using System.Reflection;

namespace ThirtyAnswers.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Returns the value of a property on the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object obj, string propertyName)
        {
            if (obj == null || string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("Object or property name cannot be null");
            }

            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found on type '{1}'", propertyName, type.FullName));
            }

            return (T)propertyInfo.GetValue(obj);
        }
    }
}