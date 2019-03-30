using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeartDrop : MonoBehaviour
{
    public float spawnHeight = 2.5f;
    public int xzRange = 600;
    public int yForce = 800;
    [SerializeField]
    GameObject Heartprefab;
    
    public void DropHeart()
    {
        GameObject heart = Instantiate(Heartprefab, transform.position + new Vector3(0, spawnHeight, 0), Heartprefab.transform.rotation);
        //Rigidbody heartRb = heart.GetComponent<Rigidbody>();
        //heartRb.AddForce(new Vector3(Random.Range(-xzRange, xzRange), yForce, Random.Range(-xzRange, xzRange)));
    }
}
