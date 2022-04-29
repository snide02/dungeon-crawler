using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame {
    public class TurnOrderManager : MonoBehaviour
    {
        [SerializeField]
        private ICollection<TurnBasedObject> objects = new List<TurnBasedObject>();

        [SerializeField]
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
}
