using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // A reference to the Enemy script belonging to the boss enemy
    private Enemy boss;

    // The slider that reflects the boss's health in the UI
    private Slider slider;    


    /* Perform all necessary setup for the health management.
     */ 
    private void Awake()
    {
        AssociateWithLevelBoss();

        // Will be re-activated externally when the boss fight begins
        // TODO uncomment gameObject.SetActive(false);
    }


    /* Can be used to assign the health bar to a boss.
     */ 
    public void AssociateWithLevelBoss()
    {
        GameObject theBoss = GameObject.FindGameObjectWithTag("Boss");

        if (theBoss != null)
        {
            boss = theBoss.GetComponent<Enemy>();
            slider = gameObject.GetComponent<Slider>();
            slider.maxValue = boss.MaxHealth;
            slider.minValue = 0;
            slider.value = slider.maxValue;
            boss.healthChangedEvent += OnBossHealthChanged;
        }
    }


    /* Called when the boss's health changes. Updates its health bar accordingly.
     */ 
    private void OnBossHealthChanged()
    {
        slider.value = boss.Health;

        // Unsubscribe if the boss died
        if(boss.Health == 0)
        {
            boss.healthChangedEvent -= OnBossHealthChanged;
            boss = null;
        }
    }
}
