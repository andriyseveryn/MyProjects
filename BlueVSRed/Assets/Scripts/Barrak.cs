using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrak : MonoBehaviour
{
    public Button Player1ToggleButton;
    public Button Player2ToggleButton;

    public GameObject player1Menu;
    public GameObject playe2Menu;

    GameMaster gm;
    private void Start()
    {
        gm = GetComponent<GameMaster>();
    }
    private void Update()
    {
        if (gm.playerTurn == 1)
        {
            Player1ToggleButton.interactable = true;
            Player2ToggleButton.interactable = false;
        }
        else
        {
            Player1ToggleButton.interactable = false;
            Player2ToggleButton.interactable = true;
        }
    }
    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }
    public void CloseMenus()
    {
        player1Menu.SetActive(false);
        playe2Menu.SetActive(false);
    }
    public void BuyItem(BarrakItem item)
    {
        if(gm.playerTurn==1 && item.cost <= gm.player1Gold)
        {
            gm.player1Gold -= item.cost;
            player1Menu.SetActive(false);
        }
        else if (gm.playerTurn == 2 && item.cost <= gm.player1Gold)
        {
            gm.player2Gold -= item.cost;
            playe2Menu.SetActive(false);
        }
        else
        {
            print("NOT ENOUGHT GOLD");
            return;
        }

        gm.UpdateGoldText();
        gm.purchasedItem = item;
        if (gm.selectedUnit != null)
        {
            gm.selectedUnit.selected = false;
            gm.selectedUnit = null;
        }
        GetCreatebleTiles();
    }
    void GetCreatebleTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            if (tile.IsClear())
            {
                tile.SetCreatable();
            }
        }
    }
}
