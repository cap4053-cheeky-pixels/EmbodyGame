using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeartDrop : MonoBehaviour
{
    public float spawnHeight = 2.5f;
    public int xzSpawnVelocityMax = 5;
    public int ySpawnVelocity = 0;
    [SerializeField]
    GameObject FullHeartprefab;
    [SerializeField]
    GameObject HalfHeartprefab;
    GameObject HeartType;
    
    public void DropHeart(string whichprefab)
    {
        HeartType = FullHeartprefab;
        if(whichprefab != "Full")
        {
            HeartType = HalfHeartprefab;
        }
        GameObject heart = Instantiate(HeartType, transform.position + new Vector3(0, spawnHeight, 0), FullHeartprefab.transform.rotation);
        Rigidbody heartRb = heart.GetComponent<Rigidbody>();
        heartRb.AddForce(new Vector3(Random.Range(-xzSpawnVelocityMax, xzSpawnVelocityMax), ySpawnVelocity, Random.Range(-xzSpawnVelocityMax, xzSpawnVelocityMax)),ForceMode.VelocityChange);
    }
}
