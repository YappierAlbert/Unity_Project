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
    private Player playerHealth;
    private BetterPatrol enemyPatrol;

    AudioManager audioManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<BetterPatrol>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()){
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
                audioManager.PlaySFX(audioManager.Sword);
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0 , playerLayer);

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
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }
}
