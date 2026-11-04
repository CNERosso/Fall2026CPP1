using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
<<<<<<< HEAD
    abstract public void OnPickup(GameObject interacter);
=======
    public virtual void OnPlayerPickup(PlayerController interactor) { }
    public virtual void OnEnemyPickup(GameObject interactor) { }

>>>>>>> 9275bdb47d3a6265590344d1bff4584853b89462
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
<<<<<<< HEAD
            OnPickup(collision.gameObject);
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
            
=======
            OnPlayerPickup(collision.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPlayerPickup(collision.collider.GetComponent<PlayerController>());
            Destroy(gameObject);
>>>>>>> 9275bdb47d3a6265590344d1bff4584853b89462
        }
    }
}
