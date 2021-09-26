using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public float MoveSpeed,JumpForce;
    public LayerMask Ground;
    private string currentAnimaton;
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
    ///////Animation States///////////
    const string PLAYER_IDLE = "idle";
    const string PLAYER_RUN = "run";
    const string PLAYER_JUMP = "jump";
    const string PLAYER_FALL = "fall";
    const string PLAYER_DOUBLEJUMP = "doublejump";
    const string PLAYER_DIE = "death";   
    
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
            FallingCheak();
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.isPlayerAlive && GameManager.canInpute)
        {
            PhysicsCheck();
            GroundMovement();
            QuickFall();
        } 
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
        if(isGround&&!isJumping)
        {
            if(MoveInput!=0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
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
                ChangeAnimationState(PLAYER_JUMP);
                rb.velocity = Vector2.up * JumpForce;           
            }
            else
            {
                if(canDoubleJump)
                {
                    isJumping=true;
                    JumpTimeCounter=JumpTime;
                    ChangeAnimationState(PLAYER_DOUBLEJUMP);
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
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
    void FallingCheak()
    {
        if(rb.velocity.y < -5.0f && !isGround)
        {
            ChangeAnimationState(PLAYER_FALL);
        }
    }
}

