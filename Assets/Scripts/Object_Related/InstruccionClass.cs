///FUNCIONALIDAD: Clase creada para almacenar el comando y el objeto de dicha instruccion.

using UnityEngine;

public class Instruccion
{
    public string comando;
    public GameObject parent;

    public Instruccion(string cmd, GameObject prnt)
    {
        comando = cmd;
        parent = prnt;
    }
}

