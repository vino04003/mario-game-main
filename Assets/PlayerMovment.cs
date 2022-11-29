/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovment : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool isFacingRight = true;
    bool isGrounded = true;
    [SerializeField] private float jumpPower = 1000;
    Rigidbody2D rb;
    // Start is called before the first frame update
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    void Update()
    {
       
    var h = Input.GetAxis("Horizontal");
 
    void Reverse()
    {
        if (isGrounded)
        {
            isFacingRight = !isFacingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
        if (h > 0 && !isFacingRight)
        {
            Reverse();
        }
        else if (h < 0 && isFacingRight)
        {
            Reverse();
        }
 
 
        if(Input.GetButtonDown("Jump")){
            rb.velocity = Vector2.up * jumpPower;
        }
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7, rb.velocity.y);
       
         if (rb.velocity.y < 0)
         {
            Debug.Log("jmp 1");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
         }
         else if (rb.velocity.y > 0 && Input.GetButton ("Jump")) {
            Debug.Log("jmp 2");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 4) * Time.deltaTime;
         }
    }
}
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovment : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    [Header("Layar Mask")]
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Jump")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("fall physics")]
    public float fallMultiplier;
    public float lowJumpMultiplier;


    //Gets Rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Moves player on x axis
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }


    
    void Update()
    {
    

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //turn twords you go
      

        //cool jump fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

       //fixed double jump bug
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        //lets player jump
        if (isGrounded == true && Input.GetKeyDown("space") && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //makes you jump higher when you hold down space
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {

            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;

            }

            
        }
        
    } 
    
}
