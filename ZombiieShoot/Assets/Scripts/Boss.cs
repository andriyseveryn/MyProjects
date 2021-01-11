using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    public Enemy[] enemies;
    public float spawnOffset;

    private int halfHealth;
    private Animator anim;

    public int damage;

    private Slider healthBar;

    public GameObject deathEffect;
    public GameObject blood;

    private SceneTransition sceneTransition;
    private void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
        healthBar = FindObjectOfType<Slider>();
        halfHealth = health/2;
        anim = GetComponent<Animator>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransition.LoadScene("Win");
        }
        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }
        Enemy randomenemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomenemy, transform.position+new Vector3(spawnOffset,spawnOffset,0), transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
