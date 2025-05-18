#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

[CustomPropertyDrawer(typeof(ColorDropdownAttribute))]
public class ColorDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ColorDropdownAttribute dropdown = attribute as ColorDropdownAttribute;

        object targetObject = property.serializedObject.targetObject;
        object colorSourceObject = GetNestedObject(dropdown.colorSourceField, targetObject);

        if (colorSourceObject is List<Color> colorList)
        {
            GUIContent[] options = new GUIContent[colorList.Count];
            for (int i = 0; i < colorList.Count; i++)
            {
                Color c = colorList[i];
                string hex = $"#{Mathf.RoundToInt(c.r * 255):X2}{Mathf.RoundToInt(c.g * 255):X2}{Mathf.RoundToInt(c.b * 255):X2}";
                Texture2D tex = MakeColorTexture(c);
                options[i] = new GUIContent(hex, tex);
            }

            int currentIndex = colorList.FindIndex(c => c.Equals(property.colorValue));
            if (currentIndex < 0) currentIndex = 0;

            int newIndex = EditorGUI.Popup(position, label, currentIndex, options);
            property.colorValue = colorList[newIndex];
        }
        else
        {
            EditorGUI.HelpBox(position, $"'{dropdown.colorSourceField}' không phải List<Color>", MessageType.Warning);
        }
    }
    private Texture2D MakeColorTexture(Color color)
    {
        int width = 10;
        int height = 10;

        Texture2D tex = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }


    private object GetNestedObject(string path, object obj)
    {
        string[] parts = path.Split('.');
        foreach (string part in parts)
        {
            if (obj == null) return null;
            FieldInfo field = obj.GetType().GetField(part, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) return null;
            obj = field.GetValue(obj);
        }
        return obj;
    }
}
#endif