using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startingTime;

    private void OnEnable()
    {
        //Subscribe to game manager timer event and player pause event
    }

    private void OnDisable()
    {
        //Unsubscribe to game manager timer event and player pause event
    }
    private void UpdateTimer(float time)
    {
        timerText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }
}

