
using UnityEngine;

[ExecuteInEditMode]
public class WorldPopullator : MonoBehaviour { //TODO: mejorar para poder codificar la ruta a realizar... 
    public GameObject cube;
    public int Xsize;
    public int Zsize;

	// Use this for initialization
	void Awake () {
        for (int i = 0; i < Xsize; i++)
        {
            for (int e = 0; e < Zsize; e++)
            {
                Instantiate(cube, new Vector3(i, 0f, e), Quaternion.identity);
            }
        }
    }
}
