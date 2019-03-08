using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int Health;
    public bool Golden;
    [SerializeField] private AudioSource heartPickupAudio;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (Golden)
            {
                if((player.GoldenHealth + Health) <= player.MaxGoldenHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    player.ChangeGoldenHealthBy(Health);
                    StartCoroutine(DestroyWithDelay());
                }
                else if (player.GoldenHealth != player.MaxGoldenHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    player.ChangeGoldenHealthBy(player.MaxGoldenHealth - player.Health);
                    StartCoroutine(DestroyWithDelay());
                }
            }
            else if(player.Health != player.MaxHealth)
            {
                if ((player.Health + Health) <= player.MaxHealth)
                {
                    player.ChangeHealthBy(Health);
                    StartCoroutine(DestroyWithDelay());
                }
                else if (player.Health != player.MaxHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    StartCoroutine(DestroyWithDelay());
                }
            }
        }
    }


    private void HideHeart()
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        collider.enabled = false;
        body.mass = 0.0f;
        renderer.enabled = false;
    }


    private IEnumerator DestroyWithDelay()
    {
        HideHeart();
        heartPickupAudio.Play();
        yield return new WaitForSeconds(heartPickupAudio.clip.length);
        Destroy(gameObject);
    }
}
