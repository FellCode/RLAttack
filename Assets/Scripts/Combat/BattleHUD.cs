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

    public GameObject AttackMenu;

    public Text attack1;
    public Text attack2;
    public Text attack3;
    public Text attack4;

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

    public void ToggleAttackMenu(bool active)
    {
        AttackMenu.SetActive(active);
    }



}
