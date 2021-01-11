using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;
    private Animator anim;
    public Transform shotPoint;
    Vector2 direction;

    public GameObject enemyBullet;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            if (Time.time > attackTime)
            {
                attackTime = Time.time + timeBetweenAttack;
                anim.SetTrigger("Attack");
            }
        }
    }
    public void RangeAttack()
    {
        direction = player.position-shotPoint.position;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        shotPoint.rotation = rotation;
        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
