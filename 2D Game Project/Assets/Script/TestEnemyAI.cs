using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint,rightpoint;

    public float Speed;
    private bool Faceleft;
    private float leftx,rightx;

    public int health;
    public int damage;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        leftx=leftpoint.position.x;
        rightx=rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(Faceleft)
        {
            rb.velocity=new Vector2(-Speed,rb.velocity.y);
            if(transform.position.x<leftx)
            {
                transform.localScale=new Vector3(-3,3,1);
                Faceleft = false;
            }
        }else
        {
            rb.velocity=new Vector2(Speed,rb.velocity.y);
            if(transform.position.x>rightx)
            {
                transform.localScale=new Vector3(3,3,1);
                Faceleft = true;
            }
        }
    }
}
