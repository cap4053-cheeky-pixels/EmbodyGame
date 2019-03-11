using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        slider = gameObject.GetComponent<Slider>();
        Boss.bossBattleStarted += AssociateWithLevelBoss;
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
            boss.healthChangedEvent += OnBossHealthChanged;
            boss.deathEvent += OnBossDied;

            slider.maxValue = boss.MaxHealth;
            slider.minValue = 0;
            slider.value = slider.maxValue;

            Show();
        }
    }


    /* Called when the boss's health changes. Updates its health bar accordingly.
     */ 
    private void OnBossHealthChanged()
    {
        slider.value = boss.Health;
    }


    /* Called when the boss dies.
     */ 
    private void OnBossDied(GameObject theBoss)
    {
        boss.healthChangedEvent -= OnBossHealthChanged;
        boss = null;
        Hide();
    }


    /* Called when the scene changes. Required for unsubscribing from the
     * previous static boss signal before the scene is loaded, since it
     * would persist and attempt to call AssociateWithLevelBoss in a
     * version of the game object that was destroyed upon scene change.
     */ 
    private void OnSceneChanged(Scene prev, Scene next)
    {
        Boss.bossBattleStarted -= AssociateWithLevelBoss;
    }
}
