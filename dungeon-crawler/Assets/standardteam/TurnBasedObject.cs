using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**

Implement  the TurnBasedObject interface in the contorller class of your objects
**/
public interface TurnBasedObject 
{
    void StartTurn();

    bool IsActive {get; set;}

    GameObject GameObject {get; set;}

}
