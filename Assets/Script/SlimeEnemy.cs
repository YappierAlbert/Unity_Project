using UnityEngine;
using System.Collections;

public class SlimeEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){

        // GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
        GetComponent<EnemyShooting>().enabled = false;
        GetComponentInParent<BetterPatrol>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // StartCoroutine(DestroyAfterDelay(2f));
        Destroy(gameObject);
        audioManager.PlaySFX(audioManager.Edead2);
    }

    // private IEnumerator DestroyAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(gameObject);  // Destroy the enemy object after the delay
    // }
}
