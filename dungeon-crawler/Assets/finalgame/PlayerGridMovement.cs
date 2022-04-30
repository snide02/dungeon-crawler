using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DungeonGame {
    public class PlayerGridMovement : MonoBehaviour
    {
        public bool atdoor = false;
        public Vector3 towhere;
        private GridOccupant GridOccupant;

        AudioSource AudioSource;
        public AudioClip footstepsSound;
        public AudioClip doorSound;
        int footstepsCounter = 2; //used to limit number of times sound effect plays
        

        // Start is called before the first frame update
        void Start()
        {
            GridOccupant = GetComponent<GridOccupant>();
            AudioSource = GetComponent<AudioSource>();
        }


        // Update is called once per frame
        void Update()
        {
            
            Vector2Int? offset = null;


            if (Input.GetKeyDown(KeyCode.A)) // 0 - left button; 1 - right button; 2 - middle button
            {

                offset = new Vector2Int(-1, 0);

            }

            if (Input.GetKeyDown(KeyCode.D)) // 0 - left button; 1 - right button; 2 - middle button
            {

                offset = new Vector2Int(1, 0);

            }

            if (Input.GetKeyDown(KeyCode.W)) // 0 - left button; 1 - right button; 2 - middle button
            {

                offset = new Vector2Int(0, 1);

            }


            if (Input.GetKeyDown(KeyCode.S)) // 0 - left button; 1 - right button; 2 - middle button
            {
                
                offset = new Vector2Int(0, -1);

            }


            if (offset != null) {
                ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();
                Predicate<Vector2Int> occipiedCellDetector = occupiedCells.Contains;
                Debug.Log("Occipied cells " + occupiedCells.Count);
                //Debug.Log("Occupied cell count " + occupiedCells.Count);
                Move(offset.Value,occipiedCellDetector);
            }
        }


        private void Move(Vector2Int offset, Predicate<Vector2Int> isCellOccupied) {
            Vector2Int start = GridOccupant.GetCenterCell();
            Vector2Int candidate = start + offset;

            if (!isCellOccupied(candidate)) {
                Vector3 position = GridOccupant.GridToWorld(candidate);
                transform.position = position;

                //play sound unless past limit
                if (footstepsCounter < 1)
                {
                    footstepsCounter++;
                } else {
                    AudioSource.PlayOneShot(footstepsSound, 0.50f);
                    footstepsCounter = 0;
                }
            } else {
                var occupants = GameManager.GridOccupantManager.GetOccupantsAt(candidate);

                foreach(var occupant in occupants) {

                    DoorTeleportDestination destination = occupant.GetComponent<DoorTeleportDestination>();

                    if (destination != null) {
                        AudioSource.PlayOneShot(doorSound, 0.75f);
                        transform.position = destination.teleportDestination; 
                    }
                }
            }
        }
    }
}
