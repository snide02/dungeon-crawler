using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame { 
    public class ClickStartTurn : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

            // Update is called once per frame
            void Update()
            {
                if (Input.GetMouseButtonDown(0)) {
                    Debug.Log("Clicked to start a turn");
                    GameManager.TurnOrderManager.ExecuteTurns();
                    Debug.Log("Clicked  turn executed");
                }
                
            }
    }
}
