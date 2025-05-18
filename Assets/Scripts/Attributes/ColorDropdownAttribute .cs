using UnityEngine;

public class ColorDropdownAttribute  : PropertyAttribute
{
    public string colorSourceField;

    public ColorDropdownAttribute(string colorSourceField)
    {
        this.colorSourceField = colorSourceField;
    }
}