///FUNCIONALIDAD: Este script permite mover un objeto y arrastrarlo al area de comandos. 

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class DragableObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region Variables
    public bool IsItDroped;    //verifica que se haya colocado en el espacio determinado.

	[HideInInspector]
	public GameObject placeholder = null; //objeto que guardará nuestra posicion en la herarquia 

    public Transform lastParent = null; //Ultimo transform del que se emparentaba con nuestro objeto a arrastrar
    public Transform placeholderParent = null;  //Objeto del que emparenta el placeholder
    public string funcion;
    Button Eliminar;
    public Canvas cv;
    #endregion 

    private void Start() 
    {
        cv = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        if (!_GameMode.GM.editorMode)
        {
            Eliminar = GameObject.Find("Borrar").GetComponent<Button>();

            if (Eliminar != null)
            {
                Eliminar.interactable = false;
            }
        }  
    }


	#region Dragbased GP
	//Al iniciar a arrastrar el objeto se asigna la posicion en la jerarquia al placeholder
	public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused) //Desactiva la funcionalidad de Arrastrar
        {
                //Se instancia el placeholder y se le asigna tamaño y posicion en la jerarquia
                placeholder = new GameObject();
                placeholder.transform.SetParent(transform.parent);
                LayoutElement le = placeholder.AddComponent<LayoutElement>();
                le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
                le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
                le.flexibleWidth = 0;
                le.flexibleHeight = 0;

                placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

                //Se le asigna valores al ultimo objeto al que se emparentaba y se almacena como el padre del placeholder
                lastParent = transform.parent;
                placeholderParent = lastParent;
                transform.SetParent(cv.transform);


			//Se asigna a IsItDroped como falso y se desactiva el bloqueo a raycast del objeto arrastrando
			IsItDroped = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

    //Se actualiza la posicion del objeto arrastrado respecto al cursor o puntero
    public void OnDrag(PointerEventData eventData)
    {
		//Debug.Log(placeholderParent);
		if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused )
        {
            transform.position = eventData.position;

			//En caso de que el placeholder tenga un padre distinto a placeholderparent se reasigna...
			if (placeholder.transform.parent != placeholderParent) placeholder.transform.SetParent(placeholderParent);
			//Se setea newSiblingIndex para verificar la posicion en la jerarquia del objeto arrastrando...
			int newSiblingIndex = placeholderParent.childCount;

        //Ciclo para determinar sobre que elemento en la jerarquia se esta situando el objeto arrastrando
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
			if (_GameMode.GM.editorMode)
            {
					if (transform.position.y > placeholderParent.GetChild(i).position.y)
                    {
						newSiblingIndex = i;
						if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) newSiblingIndex--;
                        break;

                    }
            }else

            if (transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
            //Se le asigna a placeholder el index en la jerarquia 
        }
                placeholder.transform.SetSiblingIndex(newSiblingIndex);
		}
	}

    //Al terminar de arrastrar el objeto se elimina
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused)
        {
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                //Si el objeto arrastrando se suelta en algun lado distinto al area a soltar se elimina
                if (!IsItDroped)
                {
                    Destroy(gameObject);
                }
                else
                {
                    //Se le asigna la posicion en la jerarquia y se emparenta el objeto arrastrando.
                    transform.SetParent(lastParent);
                    transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
                }
                Destroy(placeholder); //Se elimina el objeto placeholder 
        }
    }
    #endregion

    #region Touchbased GP
    //When clicked, enable the erase button. 
    public void OnPointerClick(PointerEventData eventData) 
    {
		
        if (_GameMode.GM.touchGM && !_GameMode.GM.IsItPaused)
        {
            Eliminar.interactable = true;
			if(_GameMode.GM.cmdAelimTemp != null)
			{
				_GameMode.GM.cmdAelimTemp.GetComponent<Image>().color = Color.white;
			}
			transform.GetComponent<Image>().color = Color.red;
            DeleteCmd.CmdDlt = gameObject;

        }
		_GameMode.GM.cmdAelimTemp = transform;
	}
    #endregion

}
