using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CommandListener : MonoBehaviour
{
	protected internal static CommandListener ComdListener; //pseudo-singleton
	int loopTimes;											//numero de veces a iterar
	Transform loopTmp;										//Transform de el nodo loop, que contiene los comandos a ciclar...
	public Transform cmdZone;                               //Contenedor de instrucciones...
	public Button playBtn;

	private void Awake()
	{
		ComdListener = this;
	}

	private void Start()
	{
		if (cmdZone == null) //pseudo-singleton... 
		{
			cmdZone = transform;
		}
	}

	public void Dummy() //dummy dispatcher para iniciar la ejecucion de comandos. 
	{
		playBtn.interactable = false;
		StartCoroutine(ShowCmds()); //iniciando corutina para ejecutar comandos
	}


    IEnumerator ShowCmds()
    {
		CommandValues Commd = null; //comando temporal
		 
        for (int i = 0; i < cmdZone.transform.childCount; i++) //Se iteran los comandos que esten en el contenedor...
        {
			Commd = cmdZone.transform.GetChild(i).GetComponent<CommandValues>();

			switch (Commd.tipo)     //Se determina el tipo de comando para asignarles valores especificos... 
			{
				case "render":		// Se fija el color del pincel... 
					if (Commd.comando == "CambiarColor")
					{
						DesignCommands.dscmd.rendColor = Commd.color;
					}
					break;

				case "movimiento":	//Se fija el numero de pasos a dar... 
					DesignCommands.dscmd.pasos = Commd.number;
					break;

				case "control":  //Se ajustan los valores de n de iteraciones y el contenedor del for... 
					if (Commd.comando == "loop")
					{
						
						loopTimes = Commd.loopIndex;
						Debug.Log(Commd+"/"+loopTimes);
						loopTmp = Commd.transform.Find("Up_loop/Middle_loop");
					}
					break;

				default:
					Debug.Log("comando no compatible!");
					break;
			}
			if (Commd.comando == "loop") //se llama a la corutina del ciclo... 
			{
				yield return StartCoroutine(Looper());
			}else DesignCommands.dscmd.Invoke(cmdZone.transform.GetChild(i).GetComponent<CommandValues>().comando, 0.1f); //Se llama a las funciones de los comandos... 

			yield return new WaitForSeconds(.5f); 
        }
		playBtn.interactable = true;		
	}

	IEnumerator Looper()
	{
		Debug.Log(loopTimes);
		CommandValues Loopcmd = null;
		for (int i = 0; i < loopTimes; i++)
		{
			Debug.Log(i);
			for (int j = 0; j < loopTmp.childCount; j++)
			{
				Loopcmd = loopTmp.GetChild(j).GetComponent<CommandValues>();

				switch (Loopcmd.tipo)
				{
					case "render":
						if (Loopcmd.comando == "CambiarColor")
						{
							DesignCommands.dscmd.rendColor = Loopcmd.color;
						}
						break;

					case "movimiento":
						DesignCommands.dscmd.pasos = Loopcmd.number;
						break;

					default:
						Debug.Log("comando no compatible!");
						break;
				}

				DesignCommands.dscmd.Invoke(loopTmp.GetChild(j).GetComponent<CommandValues>().comando, 0.0f);

				yield return new WaitForSeconds(.5f);
			}
		}
			yield return new WaitForSeconds(.5f);
	}
}