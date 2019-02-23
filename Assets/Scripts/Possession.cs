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
        if(collision.gameObject.tag == "Dead" && Input.GetKeyDown("space")){
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
            player.SetWeapon(deadEnemy.weapon);


            // Cleanup
            Destroy(playerModel);
            // Remove the enemy
            Destroy(collision.gameObject);
        }

    }


}
