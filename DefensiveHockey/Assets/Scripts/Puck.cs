using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.gameObject.SetActive(false);
            Game.respawnPuck = true;
            Game.targetsCount--;
            EventManager.instance.UpdateUiElements();
            Debug.Log(Game.targetsCount);
        }
        if (other.CompareTag("Enemy"))
        {
            Game.respawnPuck = true;
        }
    }
}
