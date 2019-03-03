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

    // The default scale of the boss's health UI object
    private Vector3 defaultScale;


    /* Perform all necessary setup for the health management.
     */ 
    private void Awake()
    {
        DevilBoss.bossBattleStarted += AssociateWithLevelBoss;
        defaultScale = gameObject.transform.localScale;

        Hide();
    }


    /* Hides the boss's health from the UI.
     */ 
    public void Hide()
    {
        gameObject.transform.localScale = Vector3.zero;
    }


    /* Re-displays the boss's health when appropriate.
     */ 
    public void Show()
    {
        gameObject.transform.localScale = defaultScale;
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

            Show();
        }
    }


    /* Called when the boss's health changes. Updates its health bar accordingly.
     */ 
    private void OnBossHealthChanged()
    {
        slider.value = boss.Health;

        // Unsubscribe to health changes for that boss if it died
        if(boss.Health == 0)
        {
            boss.healthChangedEvent -= OnBossHealthChanged;
            boss = null;

            Hide();
        }
    }
}
