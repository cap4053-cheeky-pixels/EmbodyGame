using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int Health;
    public bool Golden;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (Golden)
            {
                if((player.GoldenHealth + Health) <= player.MaxGoldenHealth)
                {
                    player.ChangeGoldenHealthBy(Health);
                    Destroy(transform.gameObject);
                }
                else if (player.GoldenHealth != player.MaxGoldenHealth)
                {
                    player.ChangeGoldenHealthBy(player.MaxGoldenHealth - player.Health);
                    Destroy(transform.gameObject);
                }
            }
            else
            {
                if ((player.Health + Health) <= player.MaxHealth)
                {
                    player.ChangeHealthBy(Health);
                    Destroy(transform.gameObject);
                }
                else if (player.Health != player.MaxHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    Destroy(transform.gameObject);
                }
            }
        }
    }
}
