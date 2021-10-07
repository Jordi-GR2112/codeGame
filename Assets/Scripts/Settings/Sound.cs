//Funcionalidad: clase "sound" que almacena los sonidos a reproducir por el audiomanager. 


using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]//permite hacer un array de la clase ... 
public class Sound { 

    public string Name;

    public AudioClip clip;

    [Range (0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

	public bool mute; 
}
