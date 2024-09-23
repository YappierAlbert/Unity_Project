using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        // Get the player's layer and ignore collisions between bullet and player
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>();
            
            if (playerCollider != null && bulletCollider != null)
            {
                // Ensure the bullet ignores the player's collider
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage enemy on collision
        Enemy enemyComponent = collision.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(20);
            }

            SlimeEnemy slimeEnemy = collision.GetComponent<SlimeEnemy>();
            if (slimeEnemy != null)
            {
                slimeEnemy.TakeDamage(20);
            }

            EyeEnemy EyeEnemy = collision.GetComponent<EyeEnemy>();
            if (EyeEnemy != null)
            {
                EyeEnemy.TakeDamage(20);
            }
        

        // Destroy bullet on any collision
        Destroy(gameObject);
    }
}
