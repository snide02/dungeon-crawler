using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOrderManager : MonoBehaviour
{

    private ICollection<TurnBasedObject> objects = new List<TurnBasedObject>();


    public ICollection<TurnBasedObject>  TurnObjects {get => objects;}

    public void Register(TurnBasedObject obj) {
        objects.Add(obj);
    }

    public void Unregister(TurnBasedObject obj) {
        objects.Remove(obj);
    }

    public void ExecuteTurns() {

        foreach (var obj in objects) {

            obj.StartTurn();
        }
    }







}
