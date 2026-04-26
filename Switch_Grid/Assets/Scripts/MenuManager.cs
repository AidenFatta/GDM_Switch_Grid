using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject levelManager;
    private GameObject mainCanvas;
    private GameObject settingCanvas;
    private GameObject gameUICanvas;
    private GameObject levelCompleteCanvas;
    private GameObject levelFailedCanvas;

    public TextMeshProUGUI levelCompleteTimeText;

    private GameObject Player;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        mainCanvas = GameObject.FindGameObjectWithTag("MainMenu");
        settingCanvas = GameObject.FindGameObjectWithTag("SettingsMenu");
        gameUICanvas = GameObject.FindGameObjectWithTag("GameUI");
        levelCompleteCanvas = GameObject.FindGameObjectWithTag("CompleteUI");
        levelFailedCanvas = GameObject.FindGameObjectWithTag("FailedUI");
        Debug.Log("Level Failed Canvas found level manager: " + levelFailedCanvas);
        if (mainCanvas != null) mainCanvas.gameObject.SetActive(true);
        if (gameUICanvas != null) gameUICanvas.gameObject.SetActive(true);
        if (settingCanvas != null) settingCanvas.gameObject.SetActive(false);
        if (levelCompleteCanvas != null) levelCompleteCanvas.gameObject.SetActive(false);
        if (levelFailedCanvas != null) levelFailedCanvas.gameObject.SetActive(false);

        Player = GameObject.FindGameObjectWithTag("Player");
        levelManager.GetComponent<LevelManager>().OnLevelComplete += OnLevelComplete;
        levelManager.GetComponent<LevelManager>().OnLevelFailed += OnLevelFailed;
    }

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
        if (settingCanvas != null) settingCanvas.gameObject.SetActive(true);
        if (mainCanvas != null) mainCanvas.gameObject.SetActive(false);
    }

    public void OnMenuOptionsBack()
    {
        Debug.Log("Settings back button clicked. Returning to main menu.");
        if (settingCanvas != null) settingCanvas.gameObject.SetActive(false);
        if (mainCanvas != null) mainCanvas.gameObject.SetActive(true);
    }

    public void OnGameplayOptions()
    {
        Debug.Log("Gameplay options button clicked. Implement gameplay options menu here.");
        if (settingCanvas != null) settingCanvas.gameObject.SetActive(true);
        if (gameUICanvas != null) gameUICanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
        Player.GetComponent<PlayerController>().isPaused = true;
    }

    public void OnGameplayOptionsBack()
    {
        Debug.Log("Gameplay options back button clicked. Returning to game canvas");
        if (settingCanvas != null) settingCanvas.gameObject.SetActive(false);
        if (gameUICanvas != null) gameUICanvas.gameObject.SetActive(true);
        Time.timeScale = 1f; 
        Player.GetComponent<PlayerController>().isPaused = false;
    }

    public void OnSinglePlayerBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMultiplayerBack()
    {
        //Eventually be a lobby scene where players can join and wait for the game to start.
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSingleLevelBack()
    {
        SceneManager.LoadScene("SinglePlayerSelect");
    }

    public void OnMultiplayerLevelBack()
    {
        SceneManager.LoadScene("MultiplayerSelect");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnLevelComplete(float completeTime)
    {
        Debug.Log("Menu recognizes level complete");
        if (levelCompleteCanvas != null) levelCompleteCanvas.gameObject.SetActive(true);
        if (gameUICanvas != null) gameUICanvas.gameObject .SetActive(false);
        levelCompleteTimeText.text = System.TimeSpan.FromSeconds(completeTime).ToString(@"mm\:ss\:ff");
        Player.SetActive(false);
    }

    public void OnLevelFailed()
    {
        Debug.Log("Menu recognizes level failed");
        if (levelFailedCanvas != null) levelFailedCanvas.gameObject.SetActive(true);
        if (gameUICanvas != null) gameUICanvas.gameObject.SetActive(false);
        Player.SetActive(false);
    }

    public void OnQuitClick()
    {
        Debug.Log("Quit button clicked. Exiting application.");
        Application.Quit();
    }
}
