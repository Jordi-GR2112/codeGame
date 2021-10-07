//FUNCIONALIDAD: script que almacena variables del personaje e incrementa el score ...


using UnityEngine;

public class playerSettings : MonoBehaviour
{
    protected internal int objCont = 0; //contador de objetos alzados. 
    //public GameObject exMrk;
    protected internal static playerSettings playerSetting;
    internal bool isinGoal = false;

    private void Awake()
    {
        if (playerSetting == null)
        {
            playerSetting = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == _GameMode.GM.goalPoint.name)
        {
            isinGoal = true;
        }
        else
        {
			if (col.tag == "ObjectsToPick")
			{
				col.transform.parent.transform.GetComponent<SpawnerClass>().isActive = false;
				Destroy(col.gameObject);
				AudioManager.instance.Play("Pickup");
				objCont++;
				_GameMode.GM.UpdateScore(objCont);
			}
			else
			if (col.tag == "ObjectToActivate")
			{
				//TODO: Lógica para activar objetos... 
				col.transform.GetComponentInChildren<MeshFilter>().mesh = CoinSpawner.CS.coin.transform.GetComponentInChildren<MeshFilter>().mesh;
				objCont++;
			}
        }
    }

}
