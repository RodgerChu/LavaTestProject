using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletStartPosition;

    public float bulletStartForce = 30f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.transform.position = bulletStartPosition.position;
                bullet.transform.LookAt(hit.point);
                var bulletRB = bullet.GetComponent<Rigidbody>();
                if (bulletRB == null)
                {
                    Debug.LogWarning("Smth whent wrong. Bullet must contain Rigidbody component!");
                }
                else
                {
                    bulletRB.AddForce(bullet.transform.forward * bulletStartForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}
