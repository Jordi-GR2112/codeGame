///FUNCIONALIDAD: Borra instrucciones de la lista de instrucciones, solo para gamemode por toques... 

using UnityEngine;
using UnityEngine.UI;

public class DeleteCmd : MonoBehaviour
{
    public static GameObject CmdDlt;
	DragableObj[] obj;

    public void BorrarComando() //Se borra el comando...
    {

        Destroy(CmdDlt);
        gameObject.GetComponent<Button>().interactable = false;
		//Comandos.cmd.myScrollrect.verticalNormalizedPosition = (float) ZonaComandos.self.GetComponentsInChildren<DragableObj>().Length / _GameMode.GM.maxInputs;
	}

	public void BorraTodo()
	{
		if(_GameMode.GM.IsItPaused == false)
		{
			Comandos.cmd.StopEx();

			obj = ZonaComandos.self.transform.GetComponentsInChildren<DragableObj>();
			for (int i = 0; i < obj.Length; i++)
			{
				Destroy(obj[i].gameObject);
			}

			//Comandos.cmd.myScrollrect.verticalNormalizedPosition = 0f;
		}
	}

}
