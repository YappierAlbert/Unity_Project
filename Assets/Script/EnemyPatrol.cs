using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public EnemyAttack attack;
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = PointB.transform;
    }

    void Update()
    {
        if (attack.PlayerInSight())
        {
            // Stop the enemy from moving and start the attack
            rb.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);
            return; // Skip the rest of the update to keep the enemy stationary
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        Patrol();
    }

    private void Patrol()
    {
        if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            flip();
            currentPoint = (currentPoint == PointB.transform) ? PointA.transform : PointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
