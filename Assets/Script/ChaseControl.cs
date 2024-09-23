using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    public FlyingEnemyScript[] enemyArray;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            foreach (FlyingEnemyScript enemy in enemyArray)
            {
             enemy.chase = true;   
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            foreach (FlyingEnemyScript enemy in enemyArray)
            {
             enemy.chase = false;   
            }
        }
    }
}
