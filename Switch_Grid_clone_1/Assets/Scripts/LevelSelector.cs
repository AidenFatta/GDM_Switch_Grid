using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public string levelName; 

    public void OnLevelClick()
    {
        Debug.Log("Level selected. Implement level loading logic here.");
        SceneManager.LoadScene(levelName); 
    }
}
