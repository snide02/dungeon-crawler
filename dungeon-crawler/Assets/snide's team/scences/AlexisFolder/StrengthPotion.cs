using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotion : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            UseStrengthPotion();
            Destroy(gameObject);
            pickUp = true;
        }
    }

    protected void UseStrengthPotion()
    {
        if (GetPlayer == null)
        {
            GetPlayer = new player(); //REMOVE WHEN PLAYER/CHARACTER IS MADE!! JUST FOR TESTING

            GetPlayer.damageAmount = GetPlayer.damageAmount + 2;
        }
    }
}
