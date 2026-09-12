using UnityEngine;
public class Player : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
    float MoveInput = Input.GetAxis("Horizontal");
    rb.linearVelocity = new Vector2(MoveInput * MoveSpeed, rb.linearVelocity.y);

    // Kiểm tra mặt đất ngay tại Update
    isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, groundLayer);

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
    }
    setAnimation(MoveInput);
}
    private void setAnimation(float moveInput)
    {
        if(isGrounded)
        {
            if(moveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fail");
            }
        }
    }
}
