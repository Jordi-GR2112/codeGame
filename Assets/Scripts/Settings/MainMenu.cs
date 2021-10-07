
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string Leveltoload = "ModeSelector";
    public SceneFader fader;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        fader.FadeTo(Leveltoload);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego!...");
        Application.Quit();
    }
}
