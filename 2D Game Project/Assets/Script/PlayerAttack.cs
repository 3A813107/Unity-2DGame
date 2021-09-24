using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float Attacktime;
    public float startAttackTime;
    public LayerMask whatIsEnemies;
    public int damage;
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    void Update()
    {
        if(Attacktime <=0)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY),0,whatIsEnemies);
                for(int i = 0;i < enemiesToDamage.Length;i++)
                {
                    enemiesToDamage[i].GetComponent<TestEnemyAI>().TakeDamage(damage);
                }
            }
            Attacktime = startAttackTime;
        }
        else
        {
            Attacktime -= Time.deltaTime;
        }

    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }
}
