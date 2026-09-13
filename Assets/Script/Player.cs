using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    public int Health = 100;
    //Khai báo phương thức di chuyển và nhảy
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    //khai báo phương thức kiểm tra mặt đất
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    //Khai báo phương thức Animator
    private Animator animator;
    //Khai báo phương thức nhảy kép
    public int extraJumpsValue = 1;
    private int extraJumps;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        extraJumps = extraJumpsValue;
    }
    void Update()
    {
    float MoveInput = Input.GetAxis("Horizontal");
    rb.linearVelocity = new Vector2(MoveInput * MoveSpeed, rb.linearVelocity.y);

    // Kiểm tra mặt đất ngay tại Update
    isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, groundLayer);

    if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (isGrounded) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
    }
    else if(extraJumps > 0)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        extraJumps --;
    }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Damage"))
        {
            Health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            StartCoroutine(BlinkRed());

            if(Health < 0)
            {
                Die();
            }
        }
    }
    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
