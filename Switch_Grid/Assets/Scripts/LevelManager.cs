using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timeLimit;
    private float timer;
    public event Action<float> OnTimerChange;

    public GameObject levelGoal;
    public event Action<float> OnLevelComplete;
    public event Action OnLevelFailed;

    public void Start()
    {
        timer = timeLimit;
    }
    
    private void OnEnable()
    {
            levelGoal.GetComponent<GoalScript>().OnLevelComplete += HandleLevelComplete;
    }

    private void OnDisable()
    {
            levelGoal.GetComponent<GoalScript>().OnLevelComplete -= HandleLevelComplete;
    }

    private void HandleLevelComplete()
    {
        Debug.Log("Level Complete!");
        float completeTime = timeLimit - timer;
        OnLevelComplete?.Invoke(completeTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        OnTimerChange?.Invoke(timer);
        if (timer <= 0)
        {
            Debug.Log("Time's up! Level failed.");
            OnLevelFailed?.Invoke();
        }
    }
}
