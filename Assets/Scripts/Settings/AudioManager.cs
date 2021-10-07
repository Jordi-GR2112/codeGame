///FUNCIONALIDAD: Manejador de sonidos, se encarga de reproducir y silenciar los sonidos agregados a este game object...

using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()  //agrega source audio al game object. 
    {
		DontDestroyOnLoad(gameObject);

        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() //Reproduce la musica del juego automaticamente al inicio del nivel. 
    {
        Play("Theme");
    }

    public void Play(string name) //reproduce el audio que se indique por nombre. 
    {
        Sound s =  Array.Find(sounds, sound => sound.Name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void Mute(string name , bool isMute) //silencia el audio que se indique por nombre. 
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.Log("error");
            return;
        }
        s.source.mute = isMute;
		s.mute = isMute;
    }
}
