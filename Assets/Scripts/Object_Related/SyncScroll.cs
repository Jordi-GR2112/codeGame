
using UnityEngine;
using UnityEngine.UI;

public class SyncScroll : MonoBehaviour {
    public Transform thisRect;
    public Transform otherRect;

	// Use this for initialization
	void Start () {
        BoxPopulator.BP.Populate();
        Canvas.ForceUpdateCanvases();
        thisRect.GetComponent<RectTransform>().sizeDelta = otherRect.GetComponent<RectTransform>().rect.size;
    }

}
