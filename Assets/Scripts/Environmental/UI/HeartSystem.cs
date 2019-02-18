using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    // A reference to the player this script is associated with
    private Player player;

    // The max number of hearts our game supports
    private int maxAttainableHearts;

    // The number of integer Health points each heart represents
    private int healthPerHeart;

    // The max number of heart containers the player currently has (which is <= maxAttainableHearts)
    private int maxPlayerHeartContainers;

    // An array of all heart containers on the canvas
    public Image[] heartImages;

    // All heart sprites (in our case, three: empty, half, full)
    public Sprite[] heartSprites;


    /* Called before the game starts. Sets up all necessary fields.
     */ 
    void Awake()
    {
        healthPerHeart = heartSprites.Length - 1;
        maxAttainableHearts = heartImages.Length;
        AssociateWith(GameObject.FindWithTag("Player").GetComponent<Player>());
    }


    /* Allows this heart system to be associated with the given Player reference.
     */ 
    public void AssociateWith(Player player)
    {
        this.player = player;
        player.healthChangedEvent += OnPlayerHealthChanged;
        OnPlayerHealthChanged();
    }


    /* Called when the associated Player's health or max health changes. Updates the
     * maximum number of heart containers that are to be displayed on screen, and also
     * updates the sprites for all of those containers according to the player's health.
     */ 
    void OnPlayerHealthChanged()
    {
        maxPlayerHeartContainers = (player.MaxHealth + 1) / healthPerHeart;
        SetVisibleHeartContainers();
        FillHearts();
    }


    /* Loops through all heart containers the game supports and enables/disables them according
     * to the player's current max heart containers.
     */
    void SetVisibleHeartContainers()
    {
        // Loop through all hearts
        for (int i = 0; i < maxAttainableHearts; i++)
        {
            // Enable hearts within the current capacity
            if(i < maxPlayerHeartContainers)
            {
                heartImages[i].enabled = true;
            }
            // Disable unused heart images
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
   

    /* Fills all available empty heart containers with the appropriate sprites, according to the
     * player's current health.
     */ 
    void FillHearts()
    {
        // In other words, index of the last visible heart container that's either half or full
        // e.g., if Health = 6 (3 hearts) and MaxHealth is 8 (4 hearts), last non-empty heart is at index = 7 / 2 - 1 = 2
        int indexOfLastNonemptyContainer = (int)Mathf.Max(0, (int)((player.Health + 1) / healthPerHeart - 1));
        bool evenHealth = player.Health % 2 == 0 && player.Health != 0;

        // First, we'll fill up all full hearts up to that index
        for (int i = 0; i < indexOfLastNonemptyContainer; i++)
        {
            heartImages[i].sprite = heartSprites[heartSprites.Length - 1];
        }

        // If the player has even health, then the last nonempty heart container has to be a full heart
        if (evenHealth)
        {
            heartImages[indexOfLastNonemptyContainer].sprite = heartSprites[heartSprites.Length - 1];
        }

        // Paradoxical edge case of Health = 0
        else if(player.Health == 0)
        {
            heartImages[indexOfLastNonemptyContainer].sprite = heartSprites[0];
        }

        // Last nonempty heart container is half a heart
        else
        {
            heartImages[indexOfLastNonemptyContainer].sprite = heartSprites[1];
        }

        // All others beyond the last nonempty are... well, empty!
        for (int i = indexOfLastNonemptyContainer + 1; i < maxPlayerHeartContainers; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
}
