//FUNCION: Clase con valores para el spawn point... 

using UnityEngine;

public class SpawnerClass : MonoBehaviour{
    protected internal bool isActive;
    protected internal Vector3 position;
    protected internal Quaternion rotation;

    public void Start()  //Se inicializan los valores default del spawn point...
    {
        isActive = true;
        position = transform.position;
        rotation = Quaternion.identity;
    }


}
