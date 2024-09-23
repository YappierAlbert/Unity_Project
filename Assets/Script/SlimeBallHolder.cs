using UnityEngine;

public class SlimeBallHolder : MonoBehaviour
{
    [SerializeField]private Transform enemy;

    private void Update(){
        transform.localScale = enemy.localScale;
    }
}
