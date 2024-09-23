using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField]private Transform FirePoint;
    [SerializeField]private GameObject[] SlimeBall;

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
                audioManager.PlaySFX(audioManager.Eshoot);
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack(){
        cooldownTimer = 0;
        SlimeBall[FindSlimeBall()].transform.position = FirePoint.position;
        SlimeBall[FindSlimeBall()].GetComponent<EnemyBullet>().ActivateProjectile();
    }

    private int FindSlimeBall(){
        for(int i = 0 ; i < SlimeBall.Length; i++){
            if(!SlimeBall[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0 , playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }
}