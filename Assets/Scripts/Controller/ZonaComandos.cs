///FUNCIONALIDAD: Este script ayuda a determinar el lugar en la jerarquia del objeto que se este arrastrando 
/// y tambien coloca el objeto que se esta arrastrando en el panel de comandos.

using UnityEngine;
using UnityEngine.EventSystems;

public class ZonaComandos : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected internal static Transform self;

    void Awake()
    {
        if(self == null)
        {
            self = transform;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
  
    //verifica si un objeto entra al espacio de la lista
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DragableObj de = eventData.pointerDrag.GetComponent<DragableObj>();
        if (de != null)
        {
            de.placeholderParent = transform;
        }
    }

    //verifica si un objeto sale del espacio de la lista
    public void OnPointerExit(PointerEventData eventData)  
    {
        if (eventData.pointerDrag == null) return;

        DragableObj de = eventData.pointerDrag.GetComponent<DragableObj>();
        if (de != null && de.placeholderParent == transform)
        {
            de.placeholderParent = de.lastParent;
        }
    }

    //Si se suelta el objeto que se esta arrastrando se le asigna a 
    public void OnDrop(PointerEventData eventData)
    {
        Drag d = eventData.pointerDrag.GetComponent<Drag>();
        
        if( d != null)  //Se verifica que sea un comando válido (para que no entre en un nullreferenceexception)
        {   if (self.GetComponentsInChildren<CommandValues>().Length >= _GameMode.GM.maxInputs)
			{
				eventData.pointerDrag.GetComponent<Drag>().IsItDroped = false;
				Destroy(eventData.pointerDrag.GetComponent<Drag>().ComandoTemp);
			}             
            //se agregan los nodos con comandos en la herarquia del panel
            eventData.pointerDrag.GetComponent<Drag>().IsItDroped = true;
            eventData.pointerDrag.GetComponent<Drag>().ComandoTemp.transform.SetParent(transform);
        }
        else //de caso contrario se agrega el objeto instanciado de Drag.cs
        {
            DragableObj de = eventData.pointerDrag.GetComponent<DragableObj>();
            if (de != null)
            {
				if (self.GetComponentsInChildren<CommandValues>().Length >= _GameMode.GM.maxInputs)
				{
					eventData.pointerDrag.GetComponent<DragableObj>().IsItDroped = false;
					Destroy(eventData.pointerDrag.transform);
				}
				eventData.pointerDrag.GetComponent<DragableObj>().IsItDroped = true;
                eventData.pointerDrag.transform.SetParent(transform);
                de.lastParent = transform;
            }
        }  
    }
}

