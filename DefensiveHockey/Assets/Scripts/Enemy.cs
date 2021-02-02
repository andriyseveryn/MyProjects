using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    [SerializeField] float speedAmount;

    Vector3 startPosition, movePosition;
    GameObject puck;

    private void Start()
    {
        startPosition = transform.position;
        puck = FindObjectOfType<Puck>().gameObject;
        EventManager.instance.OnEnemySpeed += BoostSpeed;
    }
    private void Update()
    {
        if (Game.puckPooshing)
        {
            Moving();
        }
        if (Game.targetsCount==0)
        {
            transform.position = startPosition;
        }
    }
    void Moving()
    {
        movePosition = Vector3.Lerp(transform.position, 
            new Vector3(puck.transform.position.x, transform.position.y, 
            transform.position.z), 
            enemySpeed * Time.deltaTime);

        transform.position = movePosition;
    }
    void BoostSpeed()
    {
        enemySpeed += speedAmount;
    }
}
