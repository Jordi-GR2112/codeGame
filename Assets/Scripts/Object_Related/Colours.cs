using UnityEngine;

[System.Serializable]
public class Colours {
    public string nombre;
    public Color color;

    protected internal static Colours colores;

    public  Colours (string name, Color colour )
    {
        name = nombre;
        colour = color;
    }
}
