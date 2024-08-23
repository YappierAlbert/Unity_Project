using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public Image healthBar;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){

        animator.SetBool("IsDead", true);

        // GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }


}
