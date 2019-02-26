using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    /* Called when an enemy has died. Spawns a heart with random probability.
    */
    public static void DropHeart(Transform t, float healthDropProbability, float heartTypeProbability)
    {
        var heartType = GameObject.Find("Half-Heart");
        var heartTypeChance = Random.Range(0f, 1f);
        if(heartTypeChance >= heartTypeProbability)
        {
            heartType = GameObject.Find("Heart");
        }

        var chance = Random.Range(0f, 1f);
        if (chance <= healthDropProbability)
        {
            var randX = Random.Range(-600, 600);
            var randZ = Random.Range(-600, 600);
            var heart = Instantiate(heartType, t.position + new Vector3(0, 2.5f, 0), heartType.transform.rotation);

            var heartRb = heart.GetComponent<Rigidbody>();

            heartRb.AddForce(new Vector3(randX, 800, randZ));
        }
    }
}
