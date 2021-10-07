using UnityEngine;

public class NubeMover : MonoBehaviour {
    public float moveoffset = 1.5f;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * moveoffset * Time.deltaTime);
	}
}
