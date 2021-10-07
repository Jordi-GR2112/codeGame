using UnityEngine;
using UnityEngine.EventSystems;

public class EditorCommandsMenu : MonoBehaviour, IPointerClickHandler {
	public GameObject[] UIElemnt;
	public GameObject FocusUIEl;

	public void OnPointerClick(PointerEventData eventData)
	{
		foreach (GameObject el in UIElemnt)
		{
			el.SetActive(false);
		}
		FocusUIEl.SetActive(true);
	}	
}
