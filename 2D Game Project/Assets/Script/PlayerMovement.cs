using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public float MoveSpeed,JumpForce;
    public Transform GroundCheck;
    public LayerMask Ground;
    public bool isGround;
    private bool isJumping;    
    private bool JumpPressed;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool canDoubleJump;
    private bool FacingRight=true;



    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Jump();        
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position,0.1f,Ground);
        GroundMovement();

    }

    void GroundMovement()
    {
        float MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2(MoveInput * MoveSpeed,rb.velocity.y);
        if(FacingRight == false && MoveInput > 0)
        {
            Flip();
        }else if(FacingRight == true && MoveInput < 0)
        {
            Flip();
        }

    }

    void Jump()
    {
        if(isGround)
        {
            canDoubleJump=true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGround)
            {
                isJumping=true;
                JumpTimeCounter=JumpTime;
                rb.velocity = Vector2.up * JumpForce;           
            }
            else
            {
                if(canDoubleJump)
                {
                    isJumping=true;
                    JumpTimeCounter=JumpTime;
                    rb.velocity = Vector2.up * JumpForce;
                    canDoubleJump=false;
                }
            }
        }
        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(JumpTimeCounter>0)
            {
                rb.velocity = Vector2.up * JumpForce;
                JumpTimeCounter-=Time.deltaTime;
            }
            else
            {
                isJumping=false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping=false;
        }
    }
    void Flip()
    {
        FacingRight=!FacingRight;
        Vector3 Scaler=transform.localScale;
        Scaler.x*=-1;
        transform.localScale=Scaler;
    }
}
