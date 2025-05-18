using System;
using System.Reflection;

public static class ReflectionUtility
{
    public static object GetValueByPath(object root, string path)
    {
        string[] parts = path.Split('.');
        object current = root;

        foreach (var part in parts)
        {
            if (current == null) return null;

            Type type = current.GetType();

            // Ưu tiên tìm field instance
            FieldInfo field = type.GetField(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null)
            {
                current = field.GetValue(current);
                continue;
            }

            // Nếu không có field, tìm property instance
            PropertyInfo prop = type.GetProperty(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null)
            {
                current = prop.GetValue(current);
                continue;
            }

            // Nếu không có field/property instance, thử field/property static
            field = type.GetField(part, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null)
            {
                current = field.GetValue(null);
                continue;
            }

            prop = type.GetProperty(part, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null)
            {
                current = prop.GetValue(null);
                continue;
            }

            // Không tìm thấy gì cả
            return null;
        }

        return current;
    }
}