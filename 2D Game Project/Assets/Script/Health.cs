using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private PlayerHealth playerhealth;

    [SerializeReference]private int maxhealth;
    [SerializeReference]private int currenthealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        playerhealth=GetComponent<PlayerHealth>();
    }
    void Update()
    {
        maxhealth=playerhealth.Maxhealth;
        currenthealth=playerhealth.health;
        if(currenthealth > maxhealth)
        {
            currenthealth=maxhealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currenthealth)
            {
                hearts[i].sprite=fullHeart;
            }
            else
            {
                hearts[i].sprite=emptyHeart;
            }
            if(i<maxhealth)
            {
                hearts[i].enabled=true;
            }
            else
            {
                hearts[i].enabled=false;
            }

        }
    }
}
