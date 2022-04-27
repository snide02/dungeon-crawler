using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APPotion : MonoBehaviour
{
    // Start is called before the first frame update
    public player GetPlayer; //
    public bool pickUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UseAPPotion();
            Destroy(gameObject);
            pickUp = true;
        }
    }

    protected void UseAPPotion()
    {
        if (GetPlayer == null)
        {
            GetPlayer = new player(); //REMOVE WHEN PLAYER/CHARACTER IS MADE!! JUST FOR TESTING

            GetPlayer.attackPoints = GetPlayer.attackPoints + 2;
        }
    }
}
