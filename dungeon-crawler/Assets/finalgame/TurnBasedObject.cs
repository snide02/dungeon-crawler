using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonGame {
    /**

    Implement  the TurnBasedObject interface in the contorller class of your objects
    **/


    public class TurnBasedObject : MonoBehaviour
    {


        public UnityAction OnStartTurn;
        private bool registered;

        void Start() {
            OnEnable();
        }

        public void StartTurn() {

            if (OnStartTurn != null) {
                OnStartTurn();
            }

        }

        void OnEnable() {
            if (GameManager.TurnOrderManager != null && !registered) {
                GameManager.TurnOrderManager.Register(this);
                registered = true;
            }
        }

        void OnDisable() {
            if (GameManager.TurnOrderManager != null && registered) {
                GameManager.TurnOrderManager.Unregister(this);
                registered = false;
            }
        }


    
    }
}
