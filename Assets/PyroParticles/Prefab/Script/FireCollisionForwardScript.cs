using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;

        public void OnCollisionEnter(Collision col)
        {
            // Damaging the player
            if(col.gameObject.CompareTag("Player") && gameObject.transform.parent.CompareTag("EnemyProjectile"))
            {
                Firebolt firebolt = gameObject.transform.parent.GetComponent<Firebolt>();
                col.gameObject.GetComponent<Player>().ChangeHealthBy(-firebolt.damage);
            }

            // Damaging the enemy
            else if (col.gameObject.CompareTag("Enemy") && gameObject.transform.parent.CompareTag("PlayerProjectile"))
            {
                Firebolt firebolt = gameObject.transform.parent.GetComponent<Firebolt>();
                col.gameObject.GetComponent<Player>().ChangeHealthBy(-firebolt.damage);
            }

            CollisionHandler.HandleCollision(gameObject, col);
        }
    }
}
