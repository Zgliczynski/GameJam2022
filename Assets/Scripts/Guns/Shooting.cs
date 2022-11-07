using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Update()
    {
        Shoot();
        DestroyBulletDelayed();
    }
    private void Shoot()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.position * Settings.bulletSpeed, ForceMode2D.Impulse);
        }
    }

    private void DestroyBulletDelayed()
    {
        //Destroy Bullet after 2 sec
        Destroy(GameObject.Find("bullet_0(Clone)"), Settings.bulletLifeTime);
    }
    
}
