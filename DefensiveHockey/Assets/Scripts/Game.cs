using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour {

    [SerializeField] GameObject[] targetObjects;

    public static bool gamePause, gameStart;
    public static bool isMoving;
    public static bool puckPooshing, respawnPuck;
    public static bool respawn;

    public static int targetsCount;
    public static int lvl;
    private void Awake()
    {
        lvl = 1;
        targetsCount = targetObjects.Length;
    }
    private void Update()
    {
        if (targetsCount == 0)
        {
            RespawnTargets();
        }
    }
    void RespawnTargets()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
           targetObjects[i].SetActive(true);
           targetsCount++;
        }
        lvl++;
        EventManager.instance.UpdateUiElements();
        EventManager.instance.EnemyBoost();
    }
    public void StartGame()
    {
        gameStart = true;
    }
}
