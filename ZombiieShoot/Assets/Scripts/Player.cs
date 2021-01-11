using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float timeBetweenAttack;
    public float health;
    private Rigidbody2D rb;
    private Animator anim;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Vector2 moveAmount;

    private SceneTransition sceneTransitions;
    private void Start()
    {
        sceneTransitions = FindObjectOfType<SceneTransition>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 inputMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveAmount = inputMove.normalized * speed;
        if (inputMove != Vector2.zero)
        {
            anim.SetBool("isRun", true);
        }
        else if(inputMove == Vector2.zero)
        {
            anim.SetBool("isRun", false);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+moveAmount*Time.fixedDeltaTime);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUi((int)health);
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Loose");
        }
    }
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation,transform);
    }
    void UpdateHealthUi(int currentHealth)
    {
        for (int i=0;i<hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            UpdateHealthUi((int)health);
        }
        health += healAmount;
        UpdateHealthUi((int)health);
    }
}
