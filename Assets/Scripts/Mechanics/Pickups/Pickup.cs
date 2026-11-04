using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    abstract public void OnPickup(GameObject interacter);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            
        }
    }
}
