using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Private Variables
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 Scaler;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    private int extraJumps;
    #endregion

    #region Public Variables
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float checkRadius;
    public float moveSpeed;
    public float jumpForce;
    public int extraJumpsValue;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Scaler = transform.localScale;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal")*moveSpeed;

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up*jumpForce;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up*jumpForce;
        } 

        if (Mathf.Abs(moveInput) > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }

        if (rb.velocity.y < -3)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput*Time.fixedDeltaTime, rb.velocity.y);
    }

    void LateUpdate()
    {
        if (moveInput > 0)
        {
            facingRight = true;
        }
        else if (moveInput < 0)
        {
            facingRight = false;
        }

        if ((facingRight && Scaler.x < 0) || (!facingRight && Scaler.x > 0))
        {
            Scaler.x *= -1;
        }

        transform.localScale = Scaler;
    }
}
