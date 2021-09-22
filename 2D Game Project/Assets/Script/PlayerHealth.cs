using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Maxhealth;
    private ScreenFlash sf;
    void Start()
    {
        health=Maxhealth;
        sf=GetComponent<ScreenFlash>();
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
            //Destroy(gameObject);
            Debug.Log("die");
        }
    }
}
