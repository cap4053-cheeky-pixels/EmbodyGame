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
                    DestroyHeartButPlayAudio();
                }
                else if (player.GoldenHealth != player.MaxGoldenHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    player.ChangeGoldenHealthBy(player.MaxGoldenHealth - player.Health);
                    DestroyHeartButPlayAudio();
                }
            }
            else if(player.Health != player.MaxHealth)
            {
                if ((player.Health + Health) <= player.MaxHealth)
                {
                    player.ChangeHealthBy(Health);
                    DestroyHeartButPlayAudio();
                }
                else if (player.Health != player.MaxHealth)
                {
                    player.ChangeHealthBy(player.MaxHealth - player.Health);
                    DestroyHeartButPlayAudio();
                }
            }
        }
    }

    private void DestroyHeartButPlayAudio()
    {
        /**
            Due to some logical/design oversight, we have to hide the heart
            and disable the collider immediately, but not destroy it
            because we want the audio to play (Which is a child of this object).
        */
        HideHeart();
        StartCoroutine(PlayAudioAndDestroyWithDelay());
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


    private IEnumerator PlayAudioAndDestroyWithDelay()
    {
        heartPickupAudio.Play();
        yield return new WaitForSeconds(heartPickupAudio.clip.length);
        Destroy(gameObject);
    }
}
