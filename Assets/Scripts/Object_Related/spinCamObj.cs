
using UnityEngine;

public class spinCamObj : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 50 * Time.deltaTime, 0);
    }
}
