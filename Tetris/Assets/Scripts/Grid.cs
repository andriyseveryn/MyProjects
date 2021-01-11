using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 20;
    [SerializeField] GameObject[,] grid;
    [SerializeField] private int rowValue;
    [SerializeField] private GameManage gameManager;
    [SerializeField] private ScoreManage scoreManager;

    private int streak=1;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[width, height];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LanaPiece(Group piece)
    {
        for (int i=0; i < piece.blocks.Length; i++)//перебір блоків в групі
        {
            StoreBlockInGrid(piece.blocks[i]);//визначаємо позицію кожного блоку в сітці
        }
        DeleteFullRows();
        if (gameManager)
        {
            gameManager.ConstructPiece();
        }
    }
    void StoreBlockInGrid(GameObject block)
    {
        Vector2 intPosition = Vectour.VectorRound(new Vector2(block.transform.position.x, block.transform.position.y));//заокруглена позиція блоку в тетроміно
        grid[(int)intPosition.x, (int)intPosition.y] = block;//визначаємо позицію блоку в сітці
    }
    public bool IsWithinAndEmpty(Vector2 boardPosition)//
    {
        if (isWithinBoard(boardPosition))
        {
            if (isEmptySpot(boardPosition))
            {
                return true;
            }
        }
        return false;
    }
    bool isEmptySpot(Vector2 boardPosition)
    {
        if (grid[(int)boardPosition.x, (int)boardPosition.y] == null)
        {
            return true;
        }
        return false;
    }
    bool isWithinBoard(Vector2 boardPosition)//визначаємо крайні границі ігрового поля
    {
        if (boardPosition.x >= 0 && boardPosition.x<width)
        {
            if (boardPosition.y>=0 && boardPosition.y < height)
            {
                return true;
            }
        }
        return false;
    }
    void DecreaseRow(int row)
    {
        for (int i=0; i < width; i++)
        {
            if (grid[i, row])
            {
                //Put the pieces into the space below
                grid[i, row - 1] = grid[i, row];
                //Make the previous space empty
                grid[i, row] = null;
                grid[i, row - 1].gameObject.transform.position += Vector3.down;
            }
        }
    }
    void DecreaseRowsAbove(int row)
    {
        for (int i =row; i < height; i++)
        {
            DecreaseRow(i);
        }
    }
    void DeleteFullRows()
    {
        for (int i=0; i<height; i++)
        {
            if (IsRowFull(i))
            {
                DeleteRow(i);
                DecreaseRowsAbove(i+1);
                i--;
                scoreManager.IncreaseScore(rowValue * streak);
                streak++;
            }
        }
        streak = 1;
    }
    void DeleteRow(int row)
    {
        for (int i=0; i < width; i++)
        {          
            Destroy(grid[i, row].gameObject);
            grid[i, row] = null;
        }
    }
    bool IsRowFull(int row)
    {
        for (int i=0; i < width; i++)
        {
            if (!grid[i, row])
            {
                return false;
            }
        }
        return true;
    }
}
