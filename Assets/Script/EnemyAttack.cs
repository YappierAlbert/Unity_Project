using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using UnityEditor;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private bool isAttacking = false;
    private Player playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && cooldownTimer >= attackCooldown && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f); 

        cooldownTimer = 0; 
        isAttacking = false;
    }

    public bool PlayerInSight()
    {
        Vector3 boxCenter = capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z);

        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("Player detected!");
            playerHealth = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }
}
