
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SoundSettings : MonoBehaviour, IPointerClickHandler
{
    public string[] sounds;
    public bool isMute = false;
    public Sprite deacImg;
	public Sprite actImg;

    private void Start()
    {

        if (deacImg == null && actImg == null)
        {
            Debug.Log(transform.name + " sin Imagen de reemplazo!");
        }

		foreach (Sound s in AudioManager.instance.sounds)
		{
			foreach (string snd in sounds)
			{
				if (s.Name == snd && s.mute)
				{
					transform.GetComponent<Image>().sprite = deacImg;
					isMute = true;
				}
			}
		}
	}

	private void FixedUpdate()
	{
		foreach(Sound s in AudioManager.instance.sounds)
		{
			foreach(string snd in sounds)
			{
				if(s.Name == snd && s.mute)
				{
					transform.GetComponent<Image>().sprite = deacImg;
				}
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) //Se hace mute a el sonido correspondiente y se cambia el icono del botón. 
    {
        isMute = !isMute;

        foreach (string sound in sounds)
        {
            AudioManager.instance.Mute(sound, isMute);
			Debug.Log(sound + "desactivado/activado");
        }

        if (deacImg == null) return;

        if (isMute)
        {
            transform.GetComponent<Image>().sprite = deacImg;
        }
        else transform.GetComponent<Image>().sprite = actImg;
    } 
}