using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class BattleHUD : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        public Slider hpSlider;
        public GameObject combatButtons;
        public GameObject attackMenu;

        public Text attack1;
        public Text attack2;
        public Text attack3;
        public Text attack4;

        public void SetupHUD(Unit unit){
            nameText.text = unit.unitName;
            levelText.text = $"Lvl {unit.unitLevel}";
            hpSlider.maxValue = unit.maxHp;
            hpSlider.value = unit.currentHp;
        }
    
        public void SetHp(int hp){
            hpSlider.value = hp;
        }

        public void ToggleMenu(bool active){
            combatButtons.SetActive(active);
        }

        public void ToggleAttackMenu(bool active)
        {
            attackMenu.SetActive(active);
        }
    }
}
