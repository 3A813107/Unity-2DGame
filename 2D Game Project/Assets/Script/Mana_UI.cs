using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana_UI : MonoBehaviour
{
    private PlayerMana playerMana;

    private int maxMana;
    private int currentMana;

    public Image[] Mana;
    public Sprite fullMana;
    public Sprite emptyMana;


    void Start()
    {
        playerMana=GetComponent<PlayerMana>();
    }
    void Update()
    {
        maxMana=playerMana.MaxMana;
        currentMana=playerMana.Mana;
        if(currentMana > maxMana)
        {
            currentMana=maxMana;
        }
        for (int i = 0; i < Mana.Length; i++)
        {
            if(i < currentMana)
            {
                Mana[i].sprite=fullMana;
            }
            else
            {
                Mana[i].sprite=emptyMana;
            }
            if(i<maxMana)
            {
                Mana[i].enabled=true;
            }
            else
            {
                Mana[i].enabled=false;
            }

        }
    }
}

