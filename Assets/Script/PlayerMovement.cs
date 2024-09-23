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

    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Update(){
        input = Input.GetAxisRaw("Horizontal");
        if (input == -1 && Mathf.Abs(transform.rotation.eulerAngles.y - 180) > 0.1f) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (input == 1 && Mathf.Abs(transform.rotation.eulerAngles.y - 0) > 0.1f) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        if(isGrounded == true && Input.GetButtonDown("Jump")){
            isJumping = true;
            audioManager.PlaySFX(audioManager.Jump);
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
