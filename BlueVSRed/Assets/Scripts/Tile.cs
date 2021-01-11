using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public float hoverAmount;

    public LayerMask obstacleLayer;

    public Color highlightedColor;
    public Color creatableColor;

    public bool isCreatable;
    public bool isWalkable;
    GameMaster gm;
    public void SetCreatable()
    {
        rend.color = creatableColor;
        isCreatable = true;
    }
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randTile = Random.Range(0, tileGraphics.Length);
        rend.sprite = tileGraphics[randTile];
        gm = FindObjectOfType<GameMaster>();
    }
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }
    public bool IsClear()
    {
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if (obstacle != null)
        {
            return false;
        }
        return true;
    }
    public void Highlite()
    {
        rend.color = highlightedColor;
        isWalkable = true;
    }
    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
        isCreatable = false;
    }
    private void OnMouseDown()
    {
        if (isWalkable && gm.selectedUnit != null)
        {
            gm.selectedUnit.Move(this.transform.position);
        }else if (isCreatable)
        {
            BarrakItem item = Instantiate(gm.purchasedItem, new Vector2(transform.position.x, transform.position.y),Quaternion.identity);
            gm.ResetTiles();
            Unit unit = item.GetComponent<Unit>();
            if (unit!=null)
            {
                unit.hasMoved = true;
                unit.hasAttacked = true;
            }
        }
    }
}
