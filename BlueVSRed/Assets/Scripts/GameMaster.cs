using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;
    public GameObject selectedUnitsSquare;
    public Image playerIndicator;
    public Sprite player1Indicator;
    public Sprite player2Indicator;

    public int player1Gold=100;
    public int player2Gold=100;

    public Text player1GoldText;
    public Text player2GoldText;

    public BarrakItem purchasedItem;

    public Vector2 statsPanelShift;
    public GameObject statsPanel;
    public Unit viewedUnit;

    public Text healthText;
    public Text armorText;
    public Text attackDamageText;
    public Text defenseDamagetext;


    public int playerTurn = 1;
    Tile[] tiles;
    public void RemoveStatePanel(Unit unit)
    {
        if (unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }
    public void ToggleStatsPanel(Unit unit)
    {
        if (!unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(true);
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
            viewedUnit = unit;
            UpdateStatesPanel();
        }
        else { statsPanel.SetActive(false); viewedUnit = null; }
    }
    public void UpdateStatesPanel()
    {
        if (viewedUnit != null)
        {
            healthText.text = viewedUnit.health.ToString();
            armorText.text = viewedUnit.armor.ToString();
            attackDamageText.text = viewedUnit.attackDamage.ToString();
            defenseDamagetext.text = viewedUnit.defenseDamage.ToString();
        }
    }
    public void MoveStatesPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit))
        {
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
        }
    }
    void GetGoldIncome(int playerTurn)
    {
        foreach (Village village in FindObjectsOfType<Village>())
        {
            if (village.playerNumber == playerTurn)
            {
                if (playerTurn == 1)
                {
                    player1Gold += village.goldPerTurn;
                }
                else
                {
                    player2Gold += village.goldPerTurn;
                }
            }
        }
        UpdateGoldText();
    }
    public void UpdateGoldText()
    {
        player1GoldText.text = player1Gold.ToString();
        player2GoldText.text = player2Gold.ToString();
    }
    private void Start()
    {
       //ggdqwdqwdqwdqwdqw
        GetGoldIncome(1);
        tiles = FindObjectsOfType<Tile>();
    }
    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
        if (selectedUnit != null)
        {
            selectedUnitsSquare.SetActive(true);
            selectedUnitsSquare.transform.position = new Vector3(selectedUnit.transform.position.x, selectedUnit.transform.position.y,1);
        }
        else
        {
            selectedUnitsSquare.SetActive(false);
        }
    }
    void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
            playerIndicator.sprite = player2Indicator;
        }
        else if(playerTurn==2)
        {
            playerTurn = 1;
            playerIndicator.sprite = player1Indicator;
        }
        GetGoldIncome(playerTurn);
        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }
        ResetTiles();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.weaponIcon.SetActive(false);
            unit.hasAttacked = false;
        }
        GetComponent<Barrak>().CloseMenus();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
