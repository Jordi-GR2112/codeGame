using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    public SceneFader fader;
    public GameObject[] UIelements;
    public GameObject Notificacion;
    protected internal static LevelSelector LS;

    private void Awake()
    {
        if (LS == null)
        {
            LS = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    

    private void Start()
    {
        if (UIelements == null)
        {
            Debug.Log("UIelements missing!");
        }
        if (Notificacion == null)
        {
            Debug.Log("Notificacion is missing!");

        }
        else
        {
            foreach (GameObject elemnt in UIelements)
            {
                elemnt.SetActive(true);
            }
            Notificacion.SetActive(false);
        }       
    }

    public void LoadLevel(string level)
    {
        fader.FadeTo(level);
    }

    public void ShowWinUI()
    {
        foreach (GameObject elemnt in UIelements)
        {
            elemnt.SetActive(false);
        }
        Notificacion.SetActive(true);
    }

    public void ReiniciarLvl()
    {

        foreach (GameObject elemnt in UIelements)
        {
            elemnt.SetActive(true);
        }
        Notificacion.SetActive(false);
        Comandos.cmd.StopEx();
    }

}
