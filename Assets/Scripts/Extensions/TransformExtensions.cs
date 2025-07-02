using UnityEngine;

public static class TransformExtensions
{
    public static void ClearChild(this Transform transform)
    {
        int count = transform.childCount;
        while (count > 0)
        {
            GameObject.Destroy(transform.GetChild(count - 1).gameObject);
            count--;
        }
    }
}