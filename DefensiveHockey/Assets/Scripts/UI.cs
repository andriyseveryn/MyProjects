using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text currentLvl;
    [SerializeField] TMP_Text nextLvl;
    [SerializeField] Image fillBar;

    float fillParts;

    private void Start()
    {
        fillParts = Game.targetsCount;
        fillBar.fillAmount = 0;
        currentLvl.text = Game.lvl.ToString();
        nextLvl.text = (Game.lvl + 1).ToString();
        EventManager.instance.OnUpdateUi += UpdateUiElements;
    }
    void UpdateUiElements()
    {
        currentLvl.text = Game.lvl.ToString();
        nextLvl.text = (Game.lvl + 1).ToString();
        fillBar.fillAmount += 0.34f;
        if (Game.targetsCount == fillParts)
            fillBar.fillAmount = 0;
    }
}
