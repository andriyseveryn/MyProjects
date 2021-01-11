using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPos;

    public float timeBetweenShots;
    private float shotTime;

    Animator cameraAnim;

    Vector2 direction;
    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
    }
    void Update()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.rotation = rotation;
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                if (!PauseMenu.isPause)
                {
                    Shoot();
                    cameraAnim.SetTrigger("Shake");
                    shotTime = Time.time + timeBetweenShots;
                }               
            }            
        }
    }
    void Shoot()
    {    
        GameObject bull = Instantiate(bullet, spawnPos.position,transform.rotation);
    }
}
