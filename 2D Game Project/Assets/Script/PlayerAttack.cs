using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackRate;
    private float nextAttackTime;
    public LayerMask whatIsEnemies;
    public int damage;
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time+1f/AttackRate;
            }

        }
        
    }
    
    void Attack()
    {
        Debug.Log("attack!");
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY),0,whatIsEnemies);
        for(int i = 0;i < enemiesToDamage.Length;i++)
        {
            enemiesToDamage[i].GetComponent<TestEnemyAI>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }
    
    
}
