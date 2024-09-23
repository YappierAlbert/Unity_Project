using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public GameObject shoot;

    public int Weapon = 0;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public bool isBlocking = false;  // Boolean to track if player is blocking
    public float blockReduction = 0.5f; // Damage reduction while blocking (50%)
    public Transform blockPoint; // New: Block point for Gizmos
    public float blockRange = 0.75f; // New: Blocking range for visualization
    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Updated: Detect if blocking
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Weapon == 0)
        {
            Weapon = 1;
            audioManager.PlaySFX(audioManager.ChangeWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.E) && Weapon == 1)
        {
            Weapon = 0;
            audioManager.PlaySFX(audioManager.ChangeWeapon);
        }

        // Block when holding right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            StartBlocking();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopBlocking();
        }

        // Handle attacks when not blocking
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) && Weapon == 0)
            {
                animator.SetTrigger("Attack");
                audioManager.PlaySFX(audioManager.Sword);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetMouseButtonDown(0) && Weapon == 1)
            {
                shoot.GetComponent<Shooting>().Shoot();
                audioManager.PlaySFX(audioManager.Magic);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // Handle melee attack
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }

            SlimeEnemy slimeEnemy = enemy.GetComponent<SlimeEnemy>();
            if (slimeEnemy != null)
            {
                slimeEnemy.TakeDamage(attackDamage);
            }

            EyeEnemy EyeEnemy = enemy.GetComponent<EyeEnemy>();
            if (EyeEnemy != null)
            {
                EyeEnemy.TakeDamage(attackDamage);
            }
        }
    }

    // Show attack and block ranges in the editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        // New: Draw a Gizmo for the block range
        if (blockPoint != null && isBlocking)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(blockPoint.position, blockRange);
        }
    }

    // Start blocking
    private void StartBlocking()
    {
        isBlocking = true;
        audioManager.PlaySFX(audioManager.Shield);
        // animator.SetBool("IsBlocking", true); // Add a blocking animation trigger in the Animator
    }

    // Stop blocking
    private void StopBlocking()
    {
        isBlocking = false;
        // animator.SetBool("IsBlocking", false);
    }
}
