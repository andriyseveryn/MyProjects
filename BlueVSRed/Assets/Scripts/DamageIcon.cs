using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIcon : MonoBehaviour
{
    public Sprite[] damageSprites;

    public float lifeTime;

    public GameObject effect;
    private void Start()
    {
        Invoke("Destruction", lifeTime);
    }
    public void Setup(int damage)
    {
        GetComponent<SpriteRenderer>().sprite = damageSprites[damage - 1];
    }
    void Destruction()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
