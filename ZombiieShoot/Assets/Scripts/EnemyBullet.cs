using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player playerScript;
    public Vector2 targetPos;

    public float speed;
    public int damage;
    private void Start()
    {
        playerScript = FindObjectOfType<Player>().GetComponent<Player>();
        targetPos = playerScript.transform.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
