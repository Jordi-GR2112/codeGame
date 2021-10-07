using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPopulator : MonoBehaviour {
    public GameObject box;
    GameObject tmp;
    public static BoxPopulator BP { private set; get; }

    private void Awake()
    {
        BP = this;
        tmp = null;
    }

    public void Populate()
    {
        for (int i = 0; i < _GameMode.GM.maxInputs; i++)
        {
            tmp = Instantiate(box, transform.position, Quaternion.identity, transform);
        }
    }

}
