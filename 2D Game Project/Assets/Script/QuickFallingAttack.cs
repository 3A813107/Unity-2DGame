using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFallingAttack : MonoBehaviour
{
    public Transform DamagePoint;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnemies;
    public int damage;

    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(DamagePoint.position, new Vector2(attackRangeX,attackRangeY),0,whatIsEnemies);
        for(int i = 0;i < enemiesToDamage.Length;i++)
        {
            enemiesToDamage[i].GetComponent<TestEnemyAI>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(DamagePoint.position, new Vector3(attackRangeX,attackRangeY,1));
    }
}
