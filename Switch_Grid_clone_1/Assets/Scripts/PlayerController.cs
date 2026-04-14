using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private float speed = 5f;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;   
    private float bulletSpeed = 7.5f;
    private float shootCooldown = 1f;
    private bool isShooting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsOwner) return;

        HandleMovement();
        HandleInputs();
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        Vector3 moveDirection = moveInput.normalized * speed * Time.deltaTime;
        transform.Translate(moveDirection);
    }

    private void HandleInputs()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShootServerRpc(mousePos);

            StartCoroutine(ShootCooldown());
        }
    }

    [ServerRpc]
    private void ShootServerRpc(Vector2 mousePos)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<NetworkObject>().Spawn();

        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
    }
}
