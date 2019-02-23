using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Dead" && Input.GetAxis("Possess") != 0){
            
            Enemy deadEnemy = collision.gameObject.GetComponent<Enemy>();
            player.MaxHealth = deadEnemy.MaxHealth;
            player.Health = deadEnemy.MaxHealth;


            // Possess the enemy model
            GameObject playerModel = transform.Find("Model").gameObject;
            GameObject enemyModel = deadEnemy.transform.Find("Model").gameObject;
            // Clone the enemy model within the player
            GameObject newModel = GameObject.Instantiate(enemyModel, transform);
            newModel.name = "Model";

            // Possess the enemy weapon
            GameObject playerWeapon = player.weapon;
            GameObject enemyWeapon = deadEnemy.weapon;
            // Clone the enemy weapon within the player
            GameObject newWeapon = GameObject.Instantiate(enemyWeapon, transform);
            player.SetWeapon(newWeapon);


            // Cleanup
            Destroy(playerModel);

            //This line triggers the error "destroying assets is not permitted to avoid data loss"...
            //Destroy(playerWeapon);
            // Remove the enemy
            Destroy(collision.gameObject);
        }

    }


}
