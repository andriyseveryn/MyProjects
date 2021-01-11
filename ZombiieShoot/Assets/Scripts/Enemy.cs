using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float timeBetweenAttack;
    public int damage;
    public float speed;

    public int pickupChance;
    public GameObject[] pickups;

    public GameObject deathEffect;
    public GameObject blood;

    [HideInInspector]
    public Transform player;
    public virtual void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPick = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPick, transform.position, transform.rotation);
            }
            //Instantiate(blood, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
