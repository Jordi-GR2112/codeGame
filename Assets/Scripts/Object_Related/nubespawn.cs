using UnityEngine;

public class nubespawn : MonoBehaviour {
    public float minZ = -3f;
    public float maxZ = 12f;
    public GameObject prefabNube;
    public float spawnRate;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawner", 1f, spawnRate);
	}
	
	void Spawner()
    {
        float tempPos = Random.Range(minZ,maxZ);
        GameObject tmp = Instantiate(prefabNube,new Vector3 (transform.position.x,transform.position.y, tempPos), Quaternion.identity);
        tmp.transform.SetParent(this.transform);
    }
}
