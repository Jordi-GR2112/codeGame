using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModificadorEscenario : MonoBehaviour, IPointerClickHandler {
	public Material Environment; //Material a modificar el escenario...
	public GameObject Props;    //Elementos a colocar en el escenario... 
	public Transform[] platforms; //plataformas a cambiar material... 

	public void OnPointerClick(PointerEventData eventData)
	{
		foreach (Transform plat in platforms)
		{
			Debug.Log("hello!");
			plat.GetComponent<Renderer>().material = Environment;
		}
	}
}
