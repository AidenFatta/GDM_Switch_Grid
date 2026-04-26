using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public event Action<GameObject> OnBulletHitEnemy;
    public GameObject creator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IEnemy>() != null && collision.gameObject != creator)
        {
            Debug.Log("Bullet hit an enemy: " + collision.gameObject.name);
            collision.gameObject.GetComponent<IEnemy>().OnHit();
            OnBulletHitEnemy?.Invoke(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
