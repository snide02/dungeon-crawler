using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOrderManager : MonoBehaviour
{

    private ICollection<TurnBasedObject> objects = new List<TurnBasedObject>();


    public ICollection<TurnBasedObject>  TurnObjects {get => objects;}

    /**
    
        Use in OnEnable() method for objects
    */
    public void registerTurnObject(TurnBasedObject obj) {
        objects.Add(obj);

    }

  /**
    
        Use in OnDisable() method for objects
    */
    public void unregisterTurnObject(TurnBasedObject obj) {
        objects.Remove(obj);

    }

    public void executeTurns() {

        foreach (var obj in objects) {

            obj.StartTurn();
        }
    }







}
