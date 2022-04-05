using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistrator : MonoBehaviour
{

    public GridOccupant occupant;

    void Start()
    {
       occupant = GetComponent<GridOccupant>(); 
    }


    void OnEnable() {
        GameManager.Player = gameObject;
    }

    void OnDisable() {
        GameManager.Player = null;
    }




}
