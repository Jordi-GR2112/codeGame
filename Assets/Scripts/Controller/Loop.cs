
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Loop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler  {

	//protected internal static Loop loopInstance;
	public Transform panel;


	private void Start()
	{
		panel = GameObject.FindGameObjectWithTag("PanelComandos").transform;

		Debug.Log(panel);
		if (panel == null)
		{
			Debug.Log("panel no asignado");
			return;
		}
	}

	private void FixedUpdate()
    {
        if (transform.childCount > 0)
        {
            transform.parent.parent.GetComponent<LayoutElement>().preferredHeight = (transform.childCount*45) + 50; //nhijos * childWidth + x;
        }
        else transform.parent.parent.GetComponent<LayoutElement>().preferredHeight = 75; //valor default... 
	}


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DragableObj dobj = eventData.pointerDrag.GetComponent<DragableObj>();
        if (dobj != null)
        {
            dobj.placeholderParent = transform;
        }

		Drag drg = eventData.pointerDrag.GetComponent<Drag>();
		if(drg != null)
		{
			drg.placeholderParent = transform;
			
		}

    }

    public void OnPointerExit(PointerEventData eventData)
    {
		if (eventData.pointerDrag == null) return;

		DragableObj dobj = eventData.pointerDrag.GetComponent<DragableObj>();

		if (dobj != null && dobj.transform.parent != transform)
		{
			dobj.placeholderParent = panel;
			dobj.placeholder.transform.SetParent(dobj.placeholderParent);
			//dobj.placeholder.transform.SetSiblingIndex(transform.parent.parent.GetSiblingIndex());

			//dobj.placeholderParent = dobj.lastParent;
		}

		Drag drg = eventData.pointerDrag.GetComponent<Drag>();
		if (drg != null)
		{
			drg.placeholderParent = panel;
			drg.placeholder.transform.SetParent(drg.placeholderParent);

		}
	}

    public void OnDrop(PointerEventData eventData)
    {
		Drag drag = eventData.pointerDrag.GetComponent<Drag>();

        if (drag != null)  //Se verifica que sea un comando válido (para que no entre en un nullreferenceexception)
        {
			if (transform.GetComponentsInChildren<CommandValues>().Length >= 4)
			{
				Destroy(drag.ComandoTemp);
			}else
			{
				//se agregan los nodos con comandos en la herarquia del panel
				eventData.pointerDrag.GetComponent<Drag>().IsItDroped = true;
				eventData.pointerDrag.GetComponent<Drag>().ComandoTemp.transform.SetParent(transform);
			}
        }
        else //de caso contrario se agrega el objeto instanciado de Drag.cs
        {
            DragableObj dobj = eventData.pointerDrag.GetComponent<DragableObj>();
            if (dobj != null)
            {
				if (eventData.pointerDrag.GetComponent<CommandValues>().comando == "loop")
				{
					Destroy(eventData.pointerDrag.gameObject);
					Destroy(dobj.placeholder);
				}
				if(transform.GetComponentsInChildren<CommandValues>().Length >= 4)
				{
					Destroy(eventData.pointerDrag.gameObject);
					Destroy(dobj.placeholder);
				}
				else
				{
					eventData.pointerDrag.GetComponent<DragableObj>().IsItDroped = true;
					eventData.pointerDrag.transform.SetParent(transform);
					dobj.lastParent = transform;
				}
            }
        }
    }

}
