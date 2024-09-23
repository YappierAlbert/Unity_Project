using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public Image healthBar;
    public PlayerCombat playerCombat; // Reference to PlayerCombat script to check if blocking

    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "DeadZone"){
            Die();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0f, 1f);
    }

    public void TakeDamage(int damage)
    {
        // Check if player is blocking
        if (playerCombat.isBlocking)
        {
            damage = Mathf.RoundToInt(damage * playerCombat.blockReduction); // Reduce damage while blocking
        }

        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        audioManager.PlaySFX(audioManager.Hit);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        audioManager.PlaySFX(audioManager.Pdead);
        healthBar.fillAmount = 0;
        this.enabled = false;
        SceneManager.LoadSceneAsync(4);
    }
}
