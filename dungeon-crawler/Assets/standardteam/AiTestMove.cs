using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiTestMove : MonoBehaviour
{
    public bool atdoor = false;
    public Vector3 towhere;
    private GridOccupant GridOccupant;
    // Start is called before the first frame update
    void Start()
    {
        GridOccupant = GetComponent<GridOccupant>();
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
            //Debug.Log("Occupied cell count " + occupiedCells.Count);
            Move(offset.Value,occipiedCellDetector);
        }

        if(atdoor == true && Input.GetKeyDown(KeyCode.Space))
        {
            teleport(towhere);
        }
    }


    private void Move(Vector2Int offset, Predicate<Vector2Int> isCellOccupied) {
        Vector2Int start = GridOccupant.GetCenterCell();
        Vector2Int candidate = start + offset;

        if (!isCellOccupied(candidate)) {
            Vector3 position = GridOccupant.GridToWorld(candidate);
            transform.position = position; 
        }
    }
    
    public void teleport(Vector3 destination)
    {
            
            transform.position = destination;
            
        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        DoorTeleportDestination destination = other.GetComponent<DoorTeleportDestination>();
        towhere = destination.teleportDestination;
        atdoor = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        atdoor = false;
    }




}
