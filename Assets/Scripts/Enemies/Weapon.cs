using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;
    private Transform _firePoint;

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        // No necesitas invocar Shoot aquí, ya que lo llamaremos desde el evento de la animación
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (bulletPrefab != null && _firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x < 0f)
            {
                // Izquierda
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                // Derecha
                bulletComponent.direction = Vector2.right;
            }
        }
    }
}
