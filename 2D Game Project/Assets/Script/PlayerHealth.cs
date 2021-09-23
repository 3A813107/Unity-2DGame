using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Maxhealth;

    private Animator anim;
    private ScreenFlash sf;
    private Rigidbody2D rb;
    void Start()
    {
        health=Maxhealth;
        sf=GetComponent<ScreenFlash>();
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health>Maxhealth)
        {
            health=Maxhealth;
        }
    }

    public void DamagePlayer(int damage)
    {
        CM_Shake.Instance.ShakeCamera(3f,0.2f);
        sf.FlashScreen();
        health -= damage;
        if(health<=0)
        {
            Die();
            Invoke("KillPlayer",0.4f);
        }
    }

    public void Die()
    {
        rb.velocity=new Vector2(0,0);
        GameManager.isPlayerAlive = false;
        anim.SetTrigger("death");
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }
}
