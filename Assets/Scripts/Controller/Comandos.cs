///FUNCIONALIDAD: Controlador de movimiento de nuestro personaje, de aquí el personaje realizará las ordenes recibidas del script SetCommands

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Comandos : MonoBehaviour
{

    #region Variables
    public static GameObject Player;
    Quaternion playerRot;
    Instruccion tempInst = new Instruccion(null, null);
    public float RotOffset;

	[HideInInspector]
    public int indexTemp = 0;

    protected internal bool isStoped = false;
    protected internal static Comandos cmd;
    public ScrollRect myScrollrect;

    //Colores para marcar cambios... 
    Color Naranja = new Color(1f,.596f, 0f, 1f);
	Color NaranjaAlter = new Color (1f, .831f, 0.572f, 1f);
    Color Azul = new Color(0.349f, 0.321f, 0.698f, 1f);
    Color verde = new Color(0.011f, 0.627f, 0.003f, 1f);
    Color Rojo = new Color(1f, 0f, 0f, 1f);
    public Material mat;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        cmd = this;
    }

    //Se asigna la rotacion inicial del jugador a playerRot y se verifica que haya jugador y marcador asignado...
    private void Start()
    {
        mat.color = verde;
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.Log("Falta asignar jugador!!");
            return;
        }
        else
        {
            playerRot = Player.transform.rotation;
        }
        if (mat == null)
        {
            Debug.Log("Falta material!");
        }
    }
    #endregion

    #region Funcionalidad varia... 
    //Es llamada para resetear la posicion y rotacion del jugador cada vez que se presiona el boton de ejecutar. 
    public void RestartPos()
    {
        if (!_GameMode.GM.IsItPaused)
        {
            Player.transform.position = _GameMode.GM.startPoint.transform.position; // se reinicia la rotacion y posicion original del jugador... 
            Player.transform.rotation = playerRot;

            playerSettings.playerSetting.isinGoal = false;
            Debug.Log("restarting position");
            CoinSpawner.CS.SpawnCoin();
        }
    }

	public void ClearColors()
	{
		if(SetCommands.SCInstance.comandos != null)
		{
			if (indexTemp > 0 && tempInst.parent != null)    //Cambia de color al elemento anterior... 
			{
				tempInst.parent.GetComponent<Image>().color = Color.white;
			}
			foreach (Instruccion cmd in SetCommands.SCInstance.comandos)
			{
				cmd.parent.GetComponent<Image>().color = Color.white;
				//Debug.Log(cmd.parent.GetComponent<Image>());
			}
		}
	}

    //Para la ejecución... 
    public void StopEx()
    {
		isStoped = true;                                //Variable para detener la execución de comandos... 

		mat.color = verde;
		mat.DisableKeyword("_EMISSION");

		SetCommands.SCInstance.StopAllCoroutines();
		SetCommands.SCInstance.pausedIndex = 0;

		_GameMode.GM.EjecutarUI.GetComponent<Button>().interactable = true;        //Se muestra el boton Ejecutar y se oculta detener...
		_GameMode.GM.UpdateScore(0);                    //Se reinicia el marcador a cero...
		_GameMode.GM.IsItPaused = false;

		playerSettings.playerSetting.objCont = 0;       //así como el contador del personaje... 

		DOTween.KillAll();
        RestartPos();
    }

    //Cambia de color (Blanco) al comando...
    void ChangeColor()
    {
		//if (indexTemp > 0)    //Cambia de color al elemento anterior... 
		//{
		//	SetCommands.SCInstance.comandos[indexTemp - 1].parent.GetComponent<Image>().color = Color.white;
		//}
		if (tempInst.parent != null)
        {
			tempInst.parent.GetComponent<Image>().color = NaranjaAlter; // se cambia al color default el ultimo comando ejecutado.
        }

        if (indexTemp < SetCommands.SCInstance.comandos.Count)
        {
            tempInst.parent = SetCommands.SCInstance.comandos[indexTemp].parent;
            SetCommands.SCInstance.comandos[indexTemp].parent.GetComponent<Image>().color = Naranja; //Color Azul...
            //myScrollrect.verticalNormalizedPosition = (float)indexTemp / SetCommands.SCInstance.comandos.Count;
            indexTemp++;
        }
    }

    //Verifica que el personaje pueda avanzar... 
    public bool VerificarMovimiento(Transform playerPs) //TODO: hacer más selectivo la verificación de movimiento... 
    {
        //exMark.SetActive(false);
        mat.color = verde;
        mat.DisableKeyword("_EMISSION");
        RaycastHit hit;

		Debug.DrawRay(playerPs.position, playerPs.forward,Color.red,1000f);

        if (Physics.Raycast(playerPs.position, playerPs.forward, out hit, maxDistance: 1f))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Obstacle")
            {
                //exMark.SetActive(true);
                mat.color = Rojo;
                mat.EnableKeyword("_EMISSION");

                AudioManager.instance.Play("InvalidMov");
                return true;
            }
            return false;
        }
        else return false;
    }
    #endregion

    #region Movimiento Direccional
    //Funciones de direccionalidad, se mueven los personajes a dada direccion... 
    public void Adelante()
    {
        if (!isStoped)
        {
            ChangeColor(); //Cambia de color al comando... 

            Vector3 tmp = Player.transform.position + Vector3.forward * 1f; //movimiento por x veces el tamaño del objeto (cubo)
            Player.transform.rotation = playerRot;                          //Ajusta la rotacion del personaje a la inicial...
            Player.transform.Rotate(0, 0 - RotOffset, 0);                     //y en base a la rotacion absoluta menos un offset se cambia la orientacion del personaje. 

            if (!VerificarMovimiento(Player.transform))
            {
                Player.transform.DOMove(tmp, 1f);
            }
        }
    }

    public void Atras()
    {
        if (!isStoped)
        {
            ChangeColor();

            Vector3 tmp = Player.transform.position + Vector3.back * 1f;
            Player.transform.rotation = playerRot;
            Player.transform.Rotate(0, 180 - RotOffset, 0);

            if (!VerificarMovimiento(Player.transform) && !isStoped)
            {
                Player.transform.DOMove(tmp, 1f);
            }
        }
    }

    public void Derecha()
    {
        if (!isStoped)
        {
            ChangeColor();

            Player.transform.rotation = playerRot;
            Player.transform.Rotate(0, 90 - RotOffset, 0);

            Vector3 tmp = Player.transform.position + Vector3.right * 1f;
            if (!VerificarMovimiento(Player.transform) && !isStoped)
            {
                Player.transform.DOMove(tmp, 1f);
            }
        }
    }

    public void Izquierda()
    {
        if (!isStoped)
        {
            ChangeColor();

            Player.transform.rotation = playerRot;
            Player.transform.Rotate(0, 270 - RotOffset, 0);

            Vector3 tmp = Player.transform.position + Vector3.left * 1f;

            if (!VerificarMovimiento(Player.transform) && !isStoped)
            {
                Player.transform.DOMove(tmp, 1f);
            }
        }
    }
    #endregion

    #region Condicionales/Acciones
    public void Accion() //TODO: definir si es atacar o saltar... 
    {
        //vacia por el momento... :'(    
    }

    public void Saltar()  //TODO: hacer salto respecto a la orientacion del personaje.... 
    {
        float tmpOr = Player.transform.rotation.y;

        Debug.Log(tmpOr);

        Vector3 tmp = Player.transform.position + Vector3.forward;
        Player.transform.DOJump(tmp, 1f, 1, 1f);
    }
    #endregion

}
