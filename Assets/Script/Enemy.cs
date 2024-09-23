using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    private bool isDead = false;

    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(isDead) return;

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("Death", true);
        isDead = true;
        audioManager.PlaySFX(audioManager.Edead);

        // Disable enemy components
        // GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponentInParent<BetterPatrol>().enabled = false;

        // Start coroutine to destroy after 2 seconds
        StartCoroutine(DestroyAfterDelay(2f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);  // Destroy the enemy object after the delay
        audioManager.PlaySFX(audioManager.Edead2);
    }
}
