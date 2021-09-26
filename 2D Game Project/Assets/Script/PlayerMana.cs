using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int Mana;
    public int MaxMana;
    public float ManaRegen;//每x秒回1魔

    private float RegenCount;
    void Start()
    {
        Mana=MaxMana; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Magcal Fire!");
            Mana--;
        }
        Regen();
    }

    void FixedUpdate()
    {
  
    }

    void Regen()
    {
        if(Mana < MaxMana)
        {
            if(RegenCount >= ManaRegen)
            {
                Mana++;
                RegenCount=0;
            }
            else
            {
                RegenCount += Time.deltaTime;
            }
        }
    }
}
