using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IEnemy
{
    private Vector2 moveInput;
    private float speed = 5f;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;
    private GameObject bullet;
    private float bulletSpeed = 7.5f;
    private float shootCooldown = 1f;
    private bool isShooting = false;

    private GameObject currentBody;

    public CinemachineCamera vcCamera;
    private bool isZoomed = false;
    private float minFOV = 5.0f;
    private float maxFOV = 10.0f;

    private GameObject menuManager;
    public bool isPaused = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        currentBody = this.gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = moveInput.normalized * speed * Time.deltaTime;
        currentBody.transform.Translate(moveDirection);
    }

    public void OnMove(InputAction.CallbackContext cntxt)
    {
        if (!isPaused)
        {
            Debug.Log("Move input received: " + cntxt.ReadValue<Vector2>());
            moveInput = cntxt.ReadValue<Vector2>();
        }
        
    }

    public void OnAttack(InputAction.CallbackContext cntxt)
    {
        if (cntxt.performed && !isShooting && !isPaused)
        {
            Debug.Log("Attack input received");
            isShooting = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            ShootBullet(mousePos);
            StartCoroutine(ShootCooldown());
        }
    }

    private void ShootBullet(Vector2 mousePos)
    {
        bullet = Instantiate(bulletPrefab, currentBody.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().creator = currentBody;
        bullet.GetComponent<BulletScript>().OnBulletHitEnemy += SwitchPlayer;
        Vector2 direction = (mousePos - (Vector2)currentBody.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
        Destroy(bullet);

    }

    private void SwitchPlayer(GameObject enemy)
    {
        Debug.Log("Switching player to " + enemy.name);
        currentBody = enemy;
        vcCamera.Follow = currentBody.transform;
    }

    public void OnInteract(InputAction.CallbackContext cntxt)
    {
        if (cntxt.started && !isPaused)
        {
            Debug.Log("Interact input started");
            if (currentBody.CompareTag("StrongBot"))
            {
                currentBody.GetComponent<Rigidbody2D>().mass = 10000f;
            }
        }

        if (cntxt.performed && !isPaused)
        {
            Debug.Log("Interact input received");
            if (currentBody.CompareTag("StrongBot"))
            {

            }
        }

        if (cntxt.canceled && !isPaused)
        {
            Debug.Log("Interact input canceled");
            if (currentBody.CompareTag("StrongBot"))
            {
                currentBody.GetComponent<Rigidbody2D>().mass = 1f; 
            }
        }

    }

    public void OnPause(InputAction.CallbackContext cntxt)
    {
        if (cntxt.performed)
        {
            Debug.Log("Pause input received");
            if (!isPaused)
            {
                if (menuManager != null)
                {
                    menuManager.GetComponent<MenuManager>().OnGameplayOptions();
                }
            }
            else if (isPaused)
            {
                if (menuManager != null)
                {
                    menuManager.GetComponent<MenuManager>().OnGameplayOptionsBack();
                }
            }        
        }
    }

    public void OnCameraToggle(InputAction.CallbackContext cntxt)
    {
        if (cntxt.performed)
        {
            if (isZoomed)
            {
                vcCamera.Lens.OrthographicSize = minFOV; // Default zoom level
                isZoomed = false;
                Debug.Log("Camera toggled:" + isZoomed);
            }
            else
            {
                vcCamera.Lens.OrthographicSize = maxFOV; // Zoomed out
                isZoomed = true;
                Debug.Log("Camera toggled:" + isZoomed);
            }
        }
    }
    public void OnHit()
    {
        Debug.Log("Enemy hit: " + gameObject.name);
    }
}
