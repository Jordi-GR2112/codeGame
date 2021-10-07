///FUNCIONALIDAD: Script que instancia el bloque de comando de la lista de comandos, este objeto es arrastrado y soltado hasta el area de comandos, donde 
///estos se convertiran en una orden para mover a nuestro personaje. 

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

// IBeginDragHandler, IDragHandler e IEndDragHandler para hacer arrastable nuestro objeto
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public bool IsItDroped;    //verifica que se haya colocado en el espacio determinado.
    public GameObject Prefab;  //Ficha a Instanciar
    public GameObject ComandoTemp;    //Ficha instanciada a mover. 

	[HideInInspector]
	public GameObject placeholder = null;

	public Transform lastparent = null;
	public Transform placeholderParent = null;
	public Transform panel;


	private void Awake()
	{
		panel = GameObject.FindGameObjectWithTag("PanelComandos").transform;

		if (panel == null)
		{
			Debug.Log("No hay panel de comandos en la escena...");
			return;
		}

	}

	#region Dragbased gameplay

	//Instancia el comando seleccionado y se le asigna al puntero. 
	public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused)
        {


			if (ZonaComandos.self.childCount < _GameMode.GM.maxInputs || ZonaComandos.self.GetComponentsInChildren<CommandValues>().Length < _GameMode.GM.maxInputs)
			{
				IsItDroped = false;

				ComandoTemp = Instantiate(Prefab, eventData.position, Quaternion.identity, transform.parent);
				if (_GameMode.GM.editorMode == true) SetValue();
				ComandoTemp.transform.SetParent(GameObject.Find("MainCanvas").transform);  // tenemos al canvas principal como maincanvas ya que usamos otro canvas para el fade de los niveles...
				ComandoTemp.transform.position = eventData.position;
				ComandoTemp.GetComponent<CanvasGroup>().blocksRaycasts = false;

				placeholder = new GameObject();
				placeholder.transform.SetParent(panel);
				LayoutElement le = placeholder.AddComponent<LayoutElement>();
				le.preferredWidth = ComandoTemp.GetComponent<LayoutElement>().preferredWidth;
				le.preferredHeight = ComandoTemp.GetComponent<LayoutElement>().preferredHeight;
				le.flexibleHeight = 0;
				le.flexibleWidth = 0;


				placeholder.transform.SetSiblingIndex(panel.childCount);

				lastparent = panel;

				placeholderParent = lastparent;
				placeholder.transform.SetParent(panel);

				ComandoTemp.GetComponent<CanvasGroup>().blocksRaycasts = false; 


			}
			else
			{
				Debug.Log("It is too much to handle for me...");
				return;
			}
        }
    }

    //Se actualiza la posicion del objeto arrastrado respecto al cursor
    public void OnDrag(PointerEventData eventData)
    {

		if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused)
        {
            if (ZonaComandos.self.childCount < _GameMode.GM.maxInputs || ZonaComandos.self.GetComponentsInChildren<CommandValues>().Length < _GameMode.GM.maxInputs)
            {

				ComandoTemp.transform.position = eventData.position;

				if (placeholder.transform.parent != placeholderParent) placeholder.transform.SetParent(placeholderParent);

				int newSiblingIndex = placeholderParent.childCount;
				
				for (int i = 0; i < placeholderParent.childCount; i++)
				{
					if (ComandoTemp.transform.position.y > placeholderParent.GetChild(i).position.y)
					{
						newSiblingIndex = i;
						if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) newSiblingIndex--;
						break;
					}
				}

				placeholder.transform.SetSiblingIndex(newSiblingIndex);
			}
        }
    }

    //Al terminar de arrastrar el objeto se determina si se coloco en un lugar valido
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused)  //El ponerle if para checar la cantidad de hijos en la zona de comandos y el numero maximo de elementos hace que no se pueda agarrar/tocar el ultimo elemento.
        {
            ComandoTemp.GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (!IsItDroped)
			{
				Destroy(ComandoTemp);
			}
			else
			{
				ComandoTemp.transform.SetParent(placeholderParent);
				ComandoTemp.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
			}
			Destroy(placeholder);
        }
    }
#endregion

	#region Touchbased gameplay
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_GameMode.GM.touchGM)
        {
            if(ZonaComandos.self.childCount < _GameMode.GM.maxInputs)
            {
                ComandoTemp = Instantiate(Prefab, transform.position, Quaternion.identity,ZonaComandos.self.transform);
				//Comandos.cmd.myScrollrect.verticalNormalizedPosition = (float) ZonaComandos.self.childCount / _GameMode.GM.maxInputs;
            } 
        }
    }
#endregion

    #region EditorMode
    public void SetValue()
    {
        int tempValue = 0;

        switch (ComandoTemp.GetComponent<CommandValues>().tipo)
        {
            //TODO: find a better way to discriminate this values!
            case "render":
                if (ComandoTemp.GetComponentInChildren<TMP_Dropdown>())
                {
                    ComandoTemp.GetComponentInChildren<TMP_Dropdown>().value = transform.GetComponentInChildren<TMP_Dropdown>().value;

                    for(int i = 0; i < _GameMode.GM.Colores.Length; i++)
                    {
                        if (i == ComandoTemp.GetComponentInChildren<TMP_Dropdown>().value)
                        {
                            ComandoTemp.GetComponent<CommandValues>().color = _GameMode.GM.Colores[i];
                        }
                    }
                }
                
                break;
            case "movimiento":
                if (ComandoTemp.GetComponentInChildren<TMP_InputField>())
                {
                    ComandoTemp.GetComponentInChildren<TMP_InputField>().text = transform.GetComponentInChildren<TMP_InputField>().text; //= transform.GetComponent<TMP_InputField>().text;
                    if (int.TryParse(ComandoTemp.GetComponentInChildren<TMP_InputField>().text, out tempValue))
                    {
                        ComandoTemp.GetComponent<CommandValues>().number = tempValue;
                    }
                }
                break;
            case "control":
                ComandoTemp.GetComponentInChildren<TMP_InputField>().text = transform.GetComponentInChildren<TMP_InputField>().text; //= transform.GetComponent<TMP_InputField>().text;
                if (int.TryParse(ComandoTemp.GetComponentInChildren<TMP_InputField>().text, out tempValue))
                {
                    ComandoTemp.GetComponent<CommandValues>().loopIndex = tempValue;
                }
                break;
        }
    }
    #endregion
}
