using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject levelManager;
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");

        if (levelManager != null)
        {
            levelManager.GetComponent<LevelManager>().OnTimerChange += UpdateTimer;
        }

    }

    private void OnDisable()
    {
        if (levelManager != null)
        {
            levelManager.GetComponent<LevelManager>().OnTimerChange += UpdateTimer;
        }
    }
    private void UpdateTimer(float time)
    {
        timerText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss\:ff");
    }
}

