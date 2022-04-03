using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/**

Implement  the TurnBasedObject interface in the contorller class of your objects
**/
public class TurnBasedObject : MonoBehaviour
{


    public UnityAction OnStartTurn;

    void Start() {
        OnEnable();
    }

    public void StartTurn() {

        if (OnStartTurn != null) {
            OnStartTurn();
        }

    }

     void OnEnable() {
        if (GameManager.TurnOrderManager != null) {
            GameManager.TurnOrderManager.Register(this);
        }
    }

    void OnDisable() {
        if (GameManager.TurnOrderManager != null) {
            GameManager.TurnOrderManager.Unregister(this);
        }
    }


   
}
