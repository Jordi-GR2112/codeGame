using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {
    public float spinOffset = 15f;

	void Update () {
        transform.Rotate(spinOffset * Time.deltaTime, 0, 0);
    }
}
