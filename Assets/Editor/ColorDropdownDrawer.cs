#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ColorDropdownAttribute))]
public class ColorDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Lấy GameManager
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null || gameManager.colors == null || gameManager.colors.Count == 0)
        {
            EditorGUI.HelpBox(position, "GameManager hoặc danh sách màu trống", MessageType.Warning);
            return;
        }

        List<Color> colorList = gameManager.colors;
        GUIContent[] options = new GUIContent[colorList.Count];

        // Tạo dropdown options
        for (int i = 0; i < colorList.Count; i++)
        {
            Texture2D tex = MakeColorTexture(colorList[i]);
            options[i] = new GUIContent($"{colorList[i].ToHex()}", tex);
        }

        // Tìm index hiện tại (dùng Color.Approximately)
        int currentIndex = colorList.FindIndex(c =>
            ColorsAreEqual(c, property.colorValue)
        );
        if (currentIndex < 0) currentIndex = 0; // Mặc định màu đầu nếu không tìm thấy

        // Hiển thị dropdown
        int newIndex = EditorGUI.Popup(position, label, currentIndex, options);

        // Cập nhật nếu có thay đổi
        if (newIndex != currentIndex)
        {
            property.colorValue = colorList[newIndex];
            property.serializedObject.ApplyModifiedProperties(); // 👈 Lưu lại!
        }
    }

    private Texture2D MakeColorTexture(Color color)
    {
        Texture2D tex = new Texture2D(16, 16);
        Color[] pixels = new Color[16 * 16];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }
    
    private bool ColorsAreEqual(Color a, Color b, float epsilon = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < epsilon &&
            Mathf.Abs(a.g - b.g) < epsilon &&
            Mathf.Abs(a.b - b.b) < epsilon &&
            Mathf.Abs(a.a - b.a) < epsilon;
    }
}
#endif