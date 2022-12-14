using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsRotateByMousePosition : MonoBehaviour
{
    private float rotationTime = 0.5f;
    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Settings.gunsRotation * Time.deltaTime * rotationTime);

        if (angle < -90 || angle > 90)
        {
            transform.localRotation = Quaternion.Euler(180, 180, -angle);
        }

    }
}