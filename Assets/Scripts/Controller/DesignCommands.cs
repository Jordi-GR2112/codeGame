
using UnityEngine;
using DG.Tweening;

public class DesignCommands : MonoBehaviour {
    public static GameObject Jugador;  
    protected internal static DesignCommands dscmd;
	public GameObject lineRenderer;
	Vector3 playerPos;
	Quaternion playerRot;


    [HideInInspector]
    public int pasos;

    public int indexTemp;
    public Color rendColor;
	public int movFix = 20;

   private void Start()
    {
        pasos = 0;

        if (dscmd == null)
        {
            dscmd = this;
        }
        else
        {
            Destroy(this);
        }

        Jugador = GameObject.FindGameObjectWithTag("Player");

        if (Jugador == null)
        {
            Debug.Log("Jugador no seleccionado");
			return;	
		}

		playerPos = Jugador.transform.position;
		playerRot = Jugador.transform.rotation;

    }


	public void ResetPos()
	{
		SubirLapiz();
		DOTween.KillAll();
		CommandListener.ComdListener.StopAllCoroutines();

		Jugador.transform.position = playerPos;
		Jugador.transform.rotation = playerRot;
		CommandListener.ComdListener.playBtn.interactable = true;
	}

    public void Mover()
    {
		//Vector3 fwd = transform.TransformDirection(Vector3.forward);

		//if (!PlayerDsng.playr.IsinCollider && Physics.Raycast(Jugador.transform.position, fwd, pasos / movFix))
		//{
			Vector3 temp = Jugador.transform.position + Jugador.transform.forward * pasos / movFix;
			Jugador.transform.DOMove(temp, .7f);
		//}
		//else return;
    }

    public void Girar()
    {
        Vector3 tempRot = Jugador.transform.localEulerAngles;
        Jugador.transform.localEulerAngles = tempRot + new Vector3 (0, pasos, 0);
    }

	public void BajarLapiz()
	{
		if (Jugador.gameObject.GetComponentInChildren<TrailRenderer>() == null)
		{
			GameObject linetmp = Instantiate(lineRenderer, Jugador.transform);
			linetmp.GetComponent<TrailRenderer>().material.SetColor("_TintColor", rendColor);
			linetmp.transform.SetParent(Jugador.transform);
		}
	}

	public void SubirLapiz()
	{
		if (Jugador.gameObject.GetComponentInChildren<TrailRenderer>() != null)
		{
			Jugador.gameObject.GetComponentInChildren<TrailRenderer>().transform.SetParent(GameObject.FindGameObjectWithTag("trailrend").transform);
		}
	}

	public void CambiarColor()
	{
		if (Jugador.gameObject.GetComponentInChildren<TrailRenderer>())
		{
			if(Jugador.gameObject.GetComponentInChildren<TrailRenderer>().material.GetColor("_TintColor") != rendColor)
			{
				SubirLapiz();
				BajarLapiz();
				Jugador.gameObject.GetComponentInChildren<TrailRenderer>().material.SetColor("_TintColor", rendColor);
			}	
		}
	}
}
