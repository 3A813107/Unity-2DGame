using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Maxhealth;
    void Start()
    {
        health=Maxhealth;
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
        health -= damage;
        if(health<=0)
        {
            //Destroy(gameObject);
            Debug.Log("die");
        }
    }
}
