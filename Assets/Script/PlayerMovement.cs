using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.2f;
    public float jumpTimeCounter;
    private bool isJumping;

    void Update(){
        input = Input.GetAxisRaw("Horizontal");
        if(input < 0){
            spriteRenderer.flipX = true;
        }
        else if(input > 0){
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        if(isGrounded == true && Input.GetButtonDown("Jump")){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetButton("Jump") && isJumping == true){
            if(jumpTimeCounter > 0){
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }else{
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump")){
            isJumping = false;
        }

    }

    void FixedUpdate(){
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(playerRb.velocity.x));
    }
}
