
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowPauseMenu : MonoBehaviour, IPointerClickHandler {
    public bool IsActive = false;
    public GameObject panelBtn;


    // Use this for initialization
    void Start() {
        if (panelBtn == null)
        {
            Debug.Log("No hay panel!");
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsActive = !IsActive;
        panelBtn.SetActive(IsActive);
    }
}
