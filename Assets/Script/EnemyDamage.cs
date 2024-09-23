using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected int damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Debug.Log("Kena");
            collision.GetComponent<Player>().TakeDamage(damage);
    }
}