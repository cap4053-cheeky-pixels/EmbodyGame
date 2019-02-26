using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // A reference to the Enemy script belonging to the boss enemy
    public Enemy boss;

    // The slider that reflects the boss's health in the UI
    private Slider slider;    


    /* Perform all necessary setup for the health management.
     */ 
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = boss.MaxHealth;
        slider.minValue = 0;
        boss.deathEvent += OnBossHealthChanged;

        // Will be re-activated externally when the boss fight begins
        gameObject.SetActive(false);
    }


    /* Called when the boss's health changes. Updates its health bar accordingly.
     */ 
    private void OnBossHealthChanged(GameObject boss)
    {
        slider.value = this.boss.Health;
    }
}
