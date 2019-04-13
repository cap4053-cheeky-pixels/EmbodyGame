using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Possession : MonoBehaviour
{
    private Player player;
    private bool canPossess = true;
    
    //what is the radius we will check for enemies to possess
    [SerializeField]
    private float possessionRadius = 1.0f;
    private GameObject targetedEnemy;
    private GameObject NewEnemytoTarget;
    private GameObject halo;
    [SerializeField]
    private GameObject TheHaloPrefab;
    private bool HaloActive = false;
    [SerializeField]
    private AudioSource PossAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //any enemy in range of Possession?
        NewEnemytoTarget = FindEnemytoPossess(possessionRadius);
        
        //If we don't have any enemy to target but we have the halo active, disable it
        if(NewEnemytoTarget == null && HaloActive)
        {
            UnHighLightEnemy();
        }
        
        //if the new enemy we need to target is not the one that is being targeted, change target
        if(NewEnemytoTarget != targetedEnemy)
        {
            HighLightEnemy();
        }
        
        //if the player has not pressed space in the previous frame then canPossess will be true
        //we do not want the player to hold down space and possess at will
        if(canPossess && Input.GetAxis("Possess") != 0)
        {
            canPossess = false;
            TakePossessionAction(targetedEnemy);
        }
        //if space is not currently pressed, ensure we can possess next frame
        else if(Input.GetAxis("Possess") == 0)
        {
            canPossess = true;
        }
        
    }
    
    void HighLightEnemy()
    {
        targetedEnemy = NewEnemytoTarget;
        HaloActive = true;
        if(halo == null)
        {
            halo = Instantiate(TheHaloPrefab);
        }
        halo.transform.SetParent(targetedEnemy.transform,false);
        halo.transform.eulerAngles = new Vector3( 90, 0, 0 );
    }
    
    void UnHighLightEnemy()
    {
        Destroy(halo);
        HaloActive = false;
        targetedEnemy = null;
    }
    
    //Actual possession takes place
    void TakePossessionAction(GameObject enemy){
        
        if(enemy == null)
        return;
        
        int heartdiff = player.Health - enemy.GetComponent<Enemy>().MaxHealth;
        
        //always assume the enemies max health
        player.MaxHealth = enemy.GetComponent<Enemy>().MaxHealth;
        
        /*if i possess an enemy, i should have no more than maxhealth
         yet i should not heal if the enemy has more health than I do currently
         */
        if(heartdiff > 0)
        {
            player.Health = enemy.GetComponent<Enemy>().MaxHealth;
        }
        //Unload Excess Hearts
        GetComponent<SimpleHeartDrop>().DropHearts(heartdiff);
        
        //update HUD
        player.ChangeMaxHealthBy(0);
        
        PossAudio.Play();
        
        //Clone the enemy model within the player
        GameObject newModel = GameObject.Instantiate(enemy.GetComponent<Entity>().model,transform);
        GameObject currentPlayerModel = player.model;
        //Possess the enemy weapon
        GameObject playerWeapon = player.GetComponent<IWeaponController>().GetWeaponInstance();
        GameObject enemyWeapon = enemy.GetComponent<IWeaponController>().GetWeaponInstance();
        //Clone the enemy weapon within the player
        GameObject newWeapon = GameObject.Instantiate(enemyWeapon, transform);
        player.SetModel(newModel);
        player.GetComponent<IWeaponController>().SetWeaponInstance(newWeapon);
        
        //Cleanup
        // Detach the model and weapons
        currentPlayerModel.transform.SetParent(null);
        playerWeapon.transform.SetParent(null);
        // Destroy the objects
        Destroy(currentPlayerModel);
        Destroy(playerWeapon);
        Destroy(enemy);
        
        // Notify any OnPossession controllers of the possesssion
        NotifyOnPossessionControllers();
    }
    
    private void NotifyOnPossessionControllers()
    {
        // Notify player controllers
        foreach(IOnPossessionController iopc in GetComponents<IOnPossessionController>())
        {
            iopc.OnPossession();
        }
    }
    
    //checks for nearby enemies
    GameObject FindEnemytoPossess(float radiuscheck){
        
        Collider[] checkWhoNearby = Physics.OverlapSphere(transform.position,radiuscheck);
        //tracks shortest distance to colliders in the vicinity of player
        float distance = 0;
        GameObject nearestEnemy = null;
        //for the foreach method
        float currentdistance;
        
        foreach(Collider other in checkWhoNearby)
        {
            currentdistance = 0;
            if(other.gameObject.tag != "Enemy")
            continue;
            if(!other.gameObject.GetComponent<Enemy>().IsDead())
            continue;
            if(!other.gameObject.GetComponent<Enemy>().isPossessable)
            continue;
            
            currentdistance = Vector3.Distance(other.gameObject.transform.position,transform.position);
            if(distance == 0)
            {
                nearestEnemy = other.gameObject;
                distance = currentdistance;
            }
            else if(currentdistance < distance)
            {
                distance = currentdistance;
                nearestEnemy = other.gameObject;
            }
        }
        return nearestEnemy;
    }
}
