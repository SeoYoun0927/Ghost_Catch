using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameManager.PlayerHit();
            Destroy(collision.gameObject);  // Bullet Á¦°Å
        }
    }
}
