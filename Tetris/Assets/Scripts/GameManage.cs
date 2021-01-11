using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private Transform ConstructPos;
    [SerializeField] private Grid grid;
    [SerializeField] private float moveTime;

    private float moveTimeSeconds;
    private Group currentGroup = null;
    // Start is called before the first frame update
    void Start()
    {
        moveTimeSeconds = moveTime;
        ConstructPiece();
    }

    // Update is called once per frame
    void Update()
    {
        moveTimeSeconds -= Time.deltaTime;  
        if (moveTimeSeconds <= 0f)
        {
            currentGroup.GameManager(Vector2.down);
            moveTimeSeconds = moveTime;
        }
    }
    GameObject GetRandomObj(GameObject[] possibleObj)
    {
        if (possibleObj.Length == 0)
        {
            return null;
        }
        int tempIndex = Random.Range(0, possibleObj.Length);
        return possibleObj[tempIndex];
    }
    public void ConstructPiece()
    {
        GameObject temp = GetRandomObj(pieces);//рандомний тетроміно
        Vector2 tempPos = Vectour.VectorRound(ConstructPos.position);//позиція спавну Round
        Group tempGroup = Instantiate(temp, tempPos, Quaternion.identity).GetComponent<Group>();//створюємо go з компонентом Group...
        tempGroup.Setup(grid);//...в Group в змінну grid записуємо створений gm
        currentGroup = tempGroup;
    }
}
