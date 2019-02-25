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

        // Will be re-activated externally when the boss fight begins
        gameObject.SetActive(false);
    }


    /* Each frame, while the game object is active, update the slider
     * to reflect the boss's health.
     */ 
    private void Update()
    {
        slider.value = boss.Health;
    }
}
