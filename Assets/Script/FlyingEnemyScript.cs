using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    public int damage = 10; // Amount of damage dealt to player on contact
    public float damageCooldown = 2f; // Cooldown time between each damage (in seconds)

    private GameObject player;
    private Rigidbody2D rb;
    private float lastDamageTime = -Mathf.Infinity; // Track last time damage was dealt

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure the Rigidbody2D doesn't interfere with movement
        rb.isKinematic = true; // Makes sure physics does not block movement
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (chase)
        {
            Chase();
        }
        else
        {
            ReturnStartPoint();
        }

        Flip();
    }

    // Enemy returns to starting point when not chasing
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    // Enemy chases the player
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Optionally, add a condition to attack when close enough
        if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            // attack
        }
    }

    // Flip the enemy's direction to face the player
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Handle damage when colliding with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TryToDamagePlayer(collision.gameObject);
        }
    }

    // Handle staying within the trigger zone
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TryToDamagePlayer(collision.gameObject);
        }
    }

    // Try to damage the player only if the cooldown has passed
    private void TryToDamagePlayer(GameObject playerObject)
    {
        if (Time.time >= lastDamageTime + damageCooldown) // Check if cooldown time has passed
        {
            playerObject.GetComponent<Player>().TakeDamage(damage);
            lastDamageTime = Time.time; // Reset the last damage time
        }
    }
}
