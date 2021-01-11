using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;
    public Tile[] tiles;

    public float moveSpeed;

    public int playerNumber;
    public int attackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;
    public GameObject weaponIcon;

    public int health;
    public int attackDamage;
    public int defenseDamage;
    public int armor;

    private Animator camAnim;

    public DamageIcon damageIcon;
    public GameObject deathEffect;

    public Text kingHealth;
    public bool isKing;

    private AudioSource source;
    public AudioClip selectedSound;
    public AudioClip moveSound;

    public GameObject victoryPanelmyHealth;
    public GameObject victoryPanelenemyHealth;

    void Start()
    {
        source = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameMaster>();
        tiles = FindObjectsOfType<Tile>();
        camAnim = Camera.main.GetComponent<Animator>();
        UpdateKingHealth();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gm.ToggleStatsPanel(this);
        }
    }
    public void UpdateKingHealth()
    {
        if (isKing)
        {
            kingHealth.text = health.ToString();
        }
    }
    void GetEnemies()
    {
        enemiesInRange.Clear();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= attackRange)
            {
                if (unit.playerNumber != gm.playerTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }
            }
        }
    }
    public void ResetWeaponIcons()
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.weaponIcon.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        ResetWeaponIcons();
        if (selected)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if (playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }
                //source.clip = selectedSound;
                source.Play();
                selected = true;
                gm.selectedUnit = this;
                
                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }           
        }
        Collider2D coll = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = coll.GetComponent<Unit>();
        if (gm.selectedUnit != null)
        {
            if(gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                gm.selectedUnit.Attack(unit);
            }
        }
    }
    void Attack(Unit enemy)
    {
        camAnim.SetTrigger("Shake");
        hasAttacked = true;
        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;
        if (enemyDamage >= 1)
        {
            DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
            instance.Setup(enemyDamage);
            enemy.health -= enemyDamage;
            enemy.UpdateKingHealth();
        }
        if (transform.tag == "Archer" && enemy.tag != "Archer")
        {
            if (Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y)<=1)
            {
                if (myDamage >= 1)
                {
                    DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                    instance.Setup(myDamage);
                    health -= myDamage;
                    UpdateKingHealth();
                }
            }
        }
        else
        {
            if (myDamage >= 1)
            {
                DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                instance.Setup(myDamage);
                health -= myDamage;
                UpdateKingHealth();
            }
        }
        
        if (enemy.health <= 0)
        {
            if (enemy.isKing == true)
            {
                enemy.victoryPanelenemyHealth.SetActive(true);
            }
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
            GetWalkableTiles();
            gm.RemoveStatePanel(enemy);
        }
        if (health <= 0)
        {
            if (isKing == true)
            {
                victoryPanelmyHealth.SetActive(true);
            }
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveStatePanel(this);
            Destroy(this.gameObject);
        }
        gm.UpdateStatesPanel();
        Debug.Log(health);
    }
    void GetWalkableTiles()
    {
        if (hasMoved)
        {
            return;
        }
        foreach (Tile tile in tiles)
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if (tile.IsClear())
                {
                    tile.Highlite();
                }
            }
        }
    }
    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }
    IEnumerator StartMovement(Vector2 tilePos)
    {
        while (transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y),moveSpeed * Time.deltaTime );
            yield return null;
        }
        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
        ResetWeaponIcons();
        GetEnemies();
        gm.MoveStatesPanel(this);
    }
}
