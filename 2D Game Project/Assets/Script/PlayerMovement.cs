using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public float MoveSpeed,JumpForce;
    public LayerMask Ground;

    public float footOffset;
    public float GroundDistance=0.2f;
    public bool isGround;
    private bool isJumping;    
    private bool QuickFallPressed;
    private float JumpTimeCounter;
    public float JumpTime;
    private bool canDoubleJump;
    private bool FacingRight=true;

    /////////////快速下降//////////////
    public float QuickFallGravity;
    public float QuickFallCoolDown;
    private float LastQuickFall=-10f;

    private bool QuickFalling;
    public float midAirDistance;
    public bool midAir;
    /////////////////////////////////////
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(GameManager.isPlayerAlive && GameManager.canInpute)
        {
            Jump();
            QuickFallCheak();
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.isPlayerAlive && GameManager.canInpute)
        {
            PhysicsCheck();
            GroundMovement();
            SwitchAnimation();
            QuickFall();
        } 
    }

    void GroundMovement()
    {
        float MoveInput = Input.GetAxisRaw("Horizontal");
        if(MoveInput > 0 || MoveInput < 0)
        {
            anim.SetBool("run",true);
        }
        else if(MoveInput==0)
        {
            anim.SetBool("run",false);
        }
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
                anim.SetBool("jump",true);
                rb.velocity = Vector2.up * JumpForce;           
            }
            else
            {
                if(canDoubleJump)
                {
                    isJumping=true;
                    JumpTimeCounter=JumpTime;
                    anim.SetBool("doublejump",true);
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
    void SwitchAnimation()
    {
        anim.SetBool("idle",false);
        if(anim.GetBool("jump"))
        {
            if(rb.velocity.y < 0.0f)
            {
                anim.SetBool("jump",false);
                anim.SetBool("fall",true);
            }
        }
        else if(isGround)
        {
            anim.SetBool("fall",false);
            anim.SetBool("idle",true);
        }

        if(anim.GetBool("run") && !isGround)
        {
            if(rb.velocity.y < -5.0f)
            {
                anim.SetBool("run",false);
                anim.SetBool("fall",true);
            }
        }
        else if(isGround)
        {
            anim.SetBool("fall",false);
            anim.SetBool("idle",true);
        }

        if(!anim.GetBool("idle"))
        {
            if(rb.velocity.y < -5.0f)
            {
                anim.SetBool("fall",true);
                anim.SetBool("idle",false);

            }
        }
        else if(isGround)
        {
            anim.SetBool("fall",false);
            anim.SetBool("idle",true);
        }

        if(anim.GetBool("doublejump"))
        {
            if(rb.velocity.y < 0.0f)
            {
                anim.SetBool("doublejump",false);
                anim.SetBool("doublefall",true);
            }
        }
        else if(isGround)
        {
            anim.SetBool("doublefall",false);
            anim.SetBool("idle",true);
        }
     
    }

    void QuickFall()
    {
        if(QuickFallPressed)
        {
            QuickFalling=true;
            LastQuickFall = Time.time;
            rb.gravityScale=QuickFallGravity;          
            if(isGround && QuickFalling)
            {
                rb.gravityScale=3;
                CM_Shake.Instance.ShakeCamera(4f,0.2f);
                QuickFallPressed=false;
                QuickFalling=false;
            }
        }
    }

    void QuickFallCheak()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && !isGround && Time.time >=(LastQuickFall+QuickFallCoolDown)&&midAir)
        {
            QuickFallPressed=true;
        }
    }
    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset,-0.6f),Vector2.down,GroundDistance,Ground);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset,-0.6f),Vector2.down,GroundDistance,Ground);
        RaycastHit2D midair = Raycast(new Vector2(0,-0.6f),Vector2.down,midAirDistance,Ground);
        midAir=!midair;
        if (leftCheck || rightCheck)
        {
            isGround = true;
        }
        else
        {
            isGround=false;
        }
    }

    RaycastHit2D Raycast(Vector2 offset,Vector2 rayDiraction,float lenght,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset ,rayDiraction , lenght , layer);
        Debug.DrawRay(pos + offset , rayDiraction*lenght,Color.blue);
        return hit;
    }

}

