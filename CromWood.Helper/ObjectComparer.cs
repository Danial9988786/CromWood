using System.Reflection;

namespace CromWood.Helper
{
    public static class ObjectComparer
    {
        public static List<string> CompareObjects<T>(T obj1, T obj2)
        {
            List<string> changedProperties = new List<string>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value1 = property.GetValue(obj1);
                object value2 = property.GetValue(obj2);

                if (!Equals(value1, value2) && (property.Name != "CreatedDate" && property.Name != "UpdatedDate" && property.Name != "UpdatedBy" && property.Name != "CreatedBy"))
                {
                    changedProperties.Add(property.Name);
                }
            }

            return changedProperties;
        }
    }
}
