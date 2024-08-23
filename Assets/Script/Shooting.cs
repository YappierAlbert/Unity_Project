using UnityEngine;

public class Shooting : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float hadap;

    void Update()
    {
        if (spriteRenderer.flipX == true)
        {
            shootingPoint.localRotation = Quaternion.Euler(0, 180, -90); 
            shootingPoint.localPosition = new Vector3(-0.35f, 0f, 0f); 
            hadap = 1;
        }
        else
        {
            shootingPoint.localRotation = Quaternion.Euler(0, 0, -90); 
            shootingPoint.localPosition = new Vector3(0.35f, 0f, 0f);
            hadap = 2;
        }

    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
