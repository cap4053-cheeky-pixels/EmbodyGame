using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Drop
{
    public GameObject drop;
    public float weight;
}

public class EnemyDrop : MonoBehaviour
{
    public List<GameObject> heartPrefabs = new List<GameObject>();
    public List<float> weights = new List<float>();
    public float healthDropProbability;
    public int xzRange = 600;
    public int yForce = 800;
    public float spawnHeight = 2.5f;

    private List<Drop> drops = new List<Drop>();

    private void Awake()
    {
        PopulateDropList();
        SubscribeToEnemyDeath();
    }

    void SubscribeToEnemyDeath()
    {
        Enemy enemy = transform.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.deathEvent += DropHeart;
        }
    }

    void PopulateDropList()
    {
        for(int i = 0; i < heartPrefabs.Count; i++)
        {
            var dropObject = new Drop
            {
                drop = heartPrefabs[i],
                weight = weights[i]
            };
            drops.Add(dropObject);
        }
    }

    /* Called when an enemy has died. Spawns a heart with random probability.
    */
    public void DropHeart(GameObject enemy)
    {
        var heartTypeChance = Random.value;
        GameObject heartType = null;
        foreach (var drop in drops)
        {
            if(heartTypeChance < drop.weight)
            {
                heartType = drop.drop;
                break;
            }
            heartTypeChance -= drop.weight;
        }

        var chance = Random.value;
        if (chance <= healthDropProbability)
        {
            var randX = Random.Range(-xzRange, xzRange);
            var randZ = Random.Range(-xzRange, xzRange);
            var heart = Instantiate(heartType, transform.position + new Vector3(0, spawnHeight, 0), heartType.transform.rotation);

            var heartRb = heart.GetComponent<Rigidbody>();

            heartRb.AddForce(new Vector3(randX, yForce, randZ));
        }
    }
}
