using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager: MonoBehaviour
{
    public static EventManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public delegate void UpdateUI();
    public delegate void BoostEnemy();

    public event UpdateUI OnUpdateUi;
    public event BoostEnemy OnEnemySpeed;

    public void UpdateUiElements()
    {
        if (OnUpdateUi != null)
        {
            OnUpdateUi();
        }
    }
    public void EnemyBoost()
    {
        if (OnEnemySpeed != null)
        {
            OnEnemySpeed();
        }
    }
}
