using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHeartDrop : MonoBehaviour
{
    public float secDelayBetweenDrops = 0.8f;
    public float spawnHeight = 2.5f;
    public int xzSpawnVelocityMax = 5;
    public int ySpawnVelocity = 0;
    [SerializeField]
    GameObject FullHeartprefab;
    [SerializeField]
    GameObject HalfHeartprefab;
    GameObject HeartType;

    public void DropHearts(int numberOfHearts)
    {
        if (numberOfHearts < 1) return;

        // Start the heart drops coroutine
        IEnumerator coroutine = DropHeartsCoroutine(numberOfHearts);
        StartCoroutine(coroutine);
    }

    // Coroutine to drop hearts in a delayed fashion
    IEnumerator DropHeartsCoroutine(int numberOfHearts)
    {
        int fullhearts = (int) (numberOfHearts / 2);
        int halfhearts = numberOfHearts % 2;

        for(int i = fullhearts; i > 0; i--)
        {
            DropHeart("Full");
            yield return new WaitForSeconds(secDelayBetweenDrops);
        }
        if(halfhearts != 0)
        {
            DropHeart("Half");
        }
        yield return null;
    }

    void DropHeart(string whichprefab)
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
