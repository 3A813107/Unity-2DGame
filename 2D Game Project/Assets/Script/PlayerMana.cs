using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int Mana;
    public int MaxMana;
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
    }
}
