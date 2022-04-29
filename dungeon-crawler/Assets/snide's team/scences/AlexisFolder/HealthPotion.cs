using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public player GetPlayer; //
    public bool pickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseHealthPotion();
            Destroy(gameObject);
            pickUp = true;
        }    
    }

    protected void UseHealthPotion()
    {
        if (GetPlayer == null)
        {
            GetPlayer = new player(); //REMOVE WHEN PLAYER/CHARACTER IS MADE!! JUST FOR TESTING
            GetPlayer.healthPoints = GetPlayer.healthPoints + 20;
            //RemoveStatusEffects();

        }
    }

    protected void RemoveStatusEffects()
    {
        //status effects will be removed here
    }
}
