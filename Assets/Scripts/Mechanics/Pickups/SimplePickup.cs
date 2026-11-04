using UnityEngine;

public class SimplePickup : MonoBehaviour
{
    public enum PickupType 
    {
        Health,
        JumpBoost,
    }

    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();

            switch (type)
            {

                case PickupType.Health:
                    controller.lives++;
                    break;


<<<<<<< HEAD
                //case PickupType.JumpBoost:
                //    controller.JumpForceChange();
                //    break;
=======
                case PickupType.JumpBoost:
                    controller.JumpForceChange();
                    break;
>>>>>>> 9275bdb47d3a6265590344d1bff4584853b89462
            }

            Destroy(gameObject);
        }

    }
}
