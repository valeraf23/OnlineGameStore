using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace OnlineGameStore.Api.Helpers
{
    public static class EnumerableExtensions
    {
        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var obj in sequence)
                action(obj);
        }

        public static IEnumerable<ExpandoObject> ShapeData<TSource>(
            this IEnumerable<TSource> source,
            string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var propertyInfoList = new List<PropertyInfo>();

            propertyInfoList.AddRange(string.IsNullOrWhiteSpace(fields)
                ? GetAllGetPropertyInfos<TSource>()
                : GetPropertyInfoForFields<TSource>(fields));

            return GetDataShapedObject(source, propertyInfoList);
        }

        private static IEnumerable<PropertyInfo> GetAllGetPropertyInfos<TSource>() => typeof(TSource)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        private static IEnumerable<ExpandoObject> GetDataShapedObject<TSource>(IEnumerable<TSource> source,
            IList<PropertyInfo> propertyInfoList)
        {
            var expandoObjectList = new List<ExpandoObject>();
            foreach (var sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    ((IDictionary<string, object>) dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }

                expandoObjectList.Add(dataShapedObject);
            }

            return expandoObjectList;
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfoForFields<TSource>(string fields)
        {
            var fieldsAfterSplit = fields.Split(',');

            var propertyInfoList = new List<PropertyInfo>();
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                }

                propertyInfoList.Add(propertyInfo);
            }

            return propertyInfoList;
        }
    }
}
