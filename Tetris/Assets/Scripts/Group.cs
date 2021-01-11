using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] public GameObject[] blocks;
    private bool isActive = true;
    private Grid grid;
    public void Setup(Grid newGrid)
    {
        grid = newGrid;
    }
     void Move(Vector2 moveDirection)
     {
        transform.position += new Vector3( moveDirection.x,moveDirection.y,0);
        RoundPosition();
     }
    void RoundPosition()
    {
        Vector2 tempVector = Vectour.VectorRound(new Vector2(transform.position.x, transform.position.y));
        transform.position = new Vector3(tempVector.x, tempVector.y, 0);
    }
    void Rotate(int rotateAmount)
    {
        transform.Rotate(0f, 0f, rotateAmount);
        RotateChildren(-rotateAmount);
        if (!isValidPos())
        {
            transform.Rotate(0f, 0f, -rotateAmount);
            RotateChildren(rotateAmount);
        }
        RoundPosition();
    }
    void RotateChildren(int amount)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.Rotate(0f, 0f, amount);
        }
    }
    public void GameManager(Vector2 moveDirection)
    {
        Move(moveDirection);
        if (!isValidPos())
        {
            Move(-moveDirection);
            grid.LanaPiece(this);
            isActive = false;
        }
    }
    void FillBlocks()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            blocks[i] = transform.GetChild(i).gameObject;
        }
    }
    bool isValidPos()
    {
        for (int i=0; i < blocks.Length; i++)
        {
            Vector2 temp =new Vector2(blocks[i].transform.position.x, blocks[i].transform.position.y);
            temp = Vectour.VectorRound(temp);
            if (!grid.IsWithinAndEmpty(temp))
            {
                return false;
            }
        }
        return true;
    }
    void Update()
    {
        if (!isActive)
        {
            return; 
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate(90);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
            if (!isValidPos())
            {
                Move(Vector2.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
            if (!isValidPos())
            {
                Move(Vector2.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down);
            if (!isValidPos())
            {       
                Move(Vector2.up);
                grid.LanaPiece(this);
                isActive = false;
            }
        }
    }
}
