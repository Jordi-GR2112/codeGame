///FUNCIONALIDAD: Este script funciona como listener a los comandos obtenidos de la linea de comandos y desde aqui se llaman a las funciones del controlador
///del personaje, desde Comandos.cs.

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetCommands : MonoBehaviour
{
    protected internal static SetCommands SCInstance;
    protected internal List<Instruccion> comandos = new List<Instruccion>(); //Lista de instrucciones que almacena los comandos introducidos al jugador
	public int pausedIndex = 0;

    public void Awake()
    {
        SCInstance = this;
    }

    public void Play(Transform cmdCont) //Inicializa la lista de comandos e inicia la ejecucion de los comandos
    {
		comandos.Clear();

		ObtenerComandos(cmdCont);
		Comandos.cmd.ClearColors();
		Comandos.cmd.indexTemp = 0;
		StartCoroutine(StartCommandList());
    } 

    //Se ejecuta la lista de comandos obtenida en ObtenerComandos
    IEnumerator  StartCommandList()//(Transform CmdCont)
    {
		if (/*!_GameMode.GM.IsItPaused &&*/ comandos.Count > 0)
		{
			Comandos.cmd.isStoped = false;

			_GameMode.GM.EjecutarUI.GetComponent<Button>().interactable = false;
            //_GameMode.GM.DetenerUI.SetActive(true);

            //Se ejecutan los comandos alojados en la lista. 
            for (int i = 0; i < comandos.Count; i++)
            {
				if (_GameMode.GM.IsItPaused)
				{
					pausedIndex = i;
					break;
				}
                Comandos.cmd.Invoke(comandos[i].comando, 0);
                yield return new WaitForSeconds(1f);
            }
            _GameMode.GM.VerificarFinNivel();
		}
        yield return new WaitForSeconds(0);
    }

    //Funcion que pobla la lista con los comandos desde el contenedor de comandos. 
    public void ObtenerComandos(Transform CmdCont)
    {
        //Ciclo que recorre todos los hijos del contenedor de comandos. 
        for (int i = 0; i < CmdCont.childCount; i++)
        {
            string tmp = CmdCont.GetChild(i).GetComponent<DragableObj>().funcion; //se le asigna a una cadena temporal la funcion de cada bloque de comandos. 

            if (tmp == "Ciclo")  //Si es un ciclo se llama a la funcion loop
            {
                for (int e = 0; e < 3; e++)
                {
                    ObtenerComandos(CmdCont.GetChild(i)); //llamada recursiva para ejecutar los nodos dentro de un loop. 
                }
            }
            else comandos.Add(new Instruccion (tmp,CmdCont.GetChild(i).gameObject));  //Se agrega el comando a la lista. 
        }
    }

	public IEnumerator ResumeComandos()
	{
		//Se ejecutan los comandos alojados en la lista. 
		if (pausedIndex >= 1 && pausedIndex < comandos.Count)
		{
			if (pausedIndex < comandos.Count - 1)
			{
				for (int i = pausedIndex; i < comandos.Count; i++)
				{
					if (_GameMode.GM.IsItPaused) break;

					pausedIndex = i;
					Debug.Log(pausedIndex);
					Comandos.cmd.Invoke(comandos[i].comando, 0);
					yield return new WaitForSeconds(1f);
				}
			}
		}

		_GameMode.GM.VerificarFinNivel();
		yield return new WaitForSeconds(1f);
	}

}
