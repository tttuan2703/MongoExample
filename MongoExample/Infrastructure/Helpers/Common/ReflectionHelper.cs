using System.Reflection;

namespace MongoExample.Infrastructure.Helpers.Common
{
    public static class ReflectionHelper
    {

        public static object GetPropertyValueByName(this Type classType, object objectClass, string propertyName)
        {
            try
            {
                var propertyInfo = GetInfoPropertyByName(classType, propertyName);
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(objectClass, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return null;
        }

        public static PropertyInfo GetInfoPropertyByName(Type classType, string propertyName)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var propertyInfo = (PropertyInfo)null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            try
            {
                var propertises = classType.GetProperties();
                if (propertises.IsNotNullNorEmpty())
                {
                    propertyInfo = propertises.FirstOrDefault(prop => prop.Name == propertyName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return propertyInfo;
        }
    }
}
