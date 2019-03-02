using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int Health;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if((player.Health + Health) <= player.MaxHealth)
            {
                player.ChangeHealthBy(Health);
                Destroy(transform.gameObject);
            }
        }
    }
}
