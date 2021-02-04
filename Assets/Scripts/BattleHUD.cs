using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;

    public Slider hpSlider;

    public GameObject CombatButtons;

    public void SetupHUD(Unit unit){
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }


    public void SetHP(int hp){
        hpSlider.value = hp;
    }

    public void ToggleMenu(bool active){
        CombatButtons.SetActive(active);
    }



}
