using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void OnSinglePlayerClick()
    {
        SceneManager.LoadScene("SinglePlayerSelect");
    }

    public void OnMultiplayerClick()
    {
        //Eventually be a lobby scene where players can join and wait for the game to start.
        SceneManager.LoadScene("MultiplayerSelect");
    }

    public void OnMenuOptionsClick()
    {
        Debug.Log("Options button clicked. Implement options menu here.");
        //On main menu need to switch the main menu canvas to the options canvas.
    }

    public void OnMenuOptionsBack()
    {
        Debug.Log("Settings back button clicked. Returning to main menu.");
        //Back Button on the options here should return the main menu to being active.
    }

    public void OnSinglePlayerBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMultiplayerBack()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnQuitClick()
    {
        Debug.Log("Quit button clicked. Exiting application.");
        Application.Quit();
    }
}
