using System;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    public event Action OnLevelComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnLevelComplete?.Invoke();
        }
    }
}
