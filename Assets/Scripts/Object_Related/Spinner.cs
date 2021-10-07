///FUNCIONALIDAD: da una animacion y movimiento al objeto que tenga el script adjunto.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public float spinOffset = 15f; //offset de grados a girar por segundo. 
    public float amp  = 0.5f;
    public float freq = 1f;

    //Almacenan la posicion del objeto...
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start () {
        posOffset = transform.position; 
	}

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.up * spinOffset * Time.deltaTime);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;

        transform.position = tempPos;
    }
}
