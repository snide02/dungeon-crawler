using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame {
    public class PlayerRegistrator : MonoBehaviour
    {

        public GridOccupant occupant {get; private set;}

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
}
