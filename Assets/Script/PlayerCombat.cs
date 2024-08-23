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
    float nextAttackTime = 0f;
    async void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Weapon == 0){
            Weapon = 1;
        }else if(Input.GetKeyDown(KeyCode.E) && Weapon == 1){
            Weapon = 0;
        }

        if(Time.time >= nextAttackTime){
            if(Input.GetMouseButtonDown(0) && Weapon == 0){
            animator.SetTrigger("Attack");
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
            }else if(Input.GetMouseButtonDown(0) && Weapon == 1){
                shoot.GetComponent<Shooting>().Shoot();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void Attack(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);     
    
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
