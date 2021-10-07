///FUNCIONALIDAD: Determina las reglas del juego... 

//TODO: agregar mas reglas, segun se vea conveniente. 
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class _GameMode : MonoBehaviour {

    #region Variables
    public bool touchGM = false; //Determina los controles de juego
    public bool editorMode = false; //Determina el modo de juego...
    public int maxInputs;        //Determina el numero de comandos a usar. 
    protected internal static _GameMode GM;
    public Transform startPoint; //Inicio de la ruta. 
    public Transform goalPoint;  //Fin de la ruta. 
    protected internal bool levelCleared = false;   //el nivel ha sido superado?
    public int PickableGoal; //Cantidad de Obj a recoger para superar nivel... 
    public GameObject EjecutarUI; //Botones...
    public GameObject DetenerUI;  
    protected internal bool IsItPaused = false; //verifica si el juego esta en pausa... 
    public GameObject pickScore;
    public ParticleSystem particles; //Particulas ganar... 
	public ParticleSystem particles2; //Particulas perder... 
	public GameObject LineRender;
	public Transform cmdAelimTemp = null;


    public Color[] Colores = new Color[9];
    #endregion

    void Awake()
    {
        #region Colores
        Colores[0] = Color.yellow;
        Colores[1] = new Color (0, 0.75f, 1, 1);
        Colores[2] = Color.white;
        Colores[3] = new Color(0.06f, 0.30f, 0.55f, 1);
        Colores[4] = new Color(1, 0.54f, 0, 1);
        Colores[5] = Color.black;
        Colores[6] = Color.red;
        Colores[7] = Color.green;
        Colores[8] = new Color(0.57f, 0.13f, 0.53f, 1);
        #endregion

        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Pausa()
    {
		if (SetCommands.SCInstance.comandos.Count >= 1 && SetCommands.SCInstance.comandos != null)
		{
			IsItPaused = !IsItPaused;

			if (IsItPaused)
			{
				DOTween.KillAll();
				StopAllCoroutines();
			}
			else
			{
				StartCoroutine(SetCommands.SCInstance.ResumeComandos());
			}
		}
    }

    public void UpdateScore(int score)
    {
        pickScore.transform.GetComponent<TMP_Text>().text = ""+score;
    }

    public void VerificarFinNivel()
    {
		//Si el jugador llega a la meta y recogio todos los objetos gana...
		//EjecutarUI.GetComponent<Button>().interactable = true;

		if (playerSettings.playerSetting.objCont == PickableGoal && playerSettings.playerSetting.isinGoal == true) //TODO: hacer el cambio entre niveles... 
        {
            AudioManager.instance.Play("Win");
            particles.Play();
            LevelSelector.LS.ShowWinUI();
            Debug.Log("Ganaste!!! Cargando siguiente nivel!");
        }
        else                                                       //TODO: mostrar feedback para indicar que el nivel no se ha terminado...
        {
            AudioManager.instance.Play("Fail");
			particles2.Play();
			Comandos.cmd.mat.color = Color.red;
            Debug.Log("Sigue así, algun día ganaras");
        }
    }
	public void Eraser()
	{
		TrailRenderer[] objetos = LineRender.transform.GetComponentsInChildren<TrailRenderer>();
		for (int i = 0; i < objetos.Length; i++)
		{
			Destroy(objetos[i]);
		}
		
	}
}
