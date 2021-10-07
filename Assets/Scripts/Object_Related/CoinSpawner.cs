//FUNCIONALIDAD: spawnea las monedas recogidas...

using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    protected internal static CoinSpawner CS;
    public GameObject coin;
    Transform[] SP;
    GameObject tmp;

    private void Awake()
    {
        if (CS == null)
        {
            CS = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SP = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            SP[i] = transform.GetChild(i).transform;
        }
    }

    public void SpawnCoin()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!SP[i].transform.GetComponent<SpawnerClass>().isActive)
            {
                tmp = Instantiate(coin, SP[i].transform.GetComponent<SpawnerClass>().position, SP[i].transform.rotation);
                tmp.transform.SetParent(SP[i].transform);
                SP[i].transform.GetComponent<SpawnerClass>().isActive = true;
            }
        }
    }
}
