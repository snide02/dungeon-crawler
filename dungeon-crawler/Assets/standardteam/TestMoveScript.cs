using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TestMoveScript : MonoBehaviour
{
    public Camera camera;
    public MovementBehavior movementBehavior;
    public GridOccupant gridOccupant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2Int startPos = gridOccupant.getOccupiedCells()[0];
        int maxSteps = 3;
         //Debug.Log("Update Pos");  
        if (Input.GetMouseButtonDown(0)) {

            Debug.Log("Click Pos " + startPos);   
            Vector3 worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
             Debug.Log("Target Pos" + target);
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, 3);
          

            Vector3 finished = gridOccupant.GridToWorld(data.CellPosition);
             Debug.Log("Finished Coords" + finished);
            
            transform.position = finished;
             Debug.Log("Current Coords" + transform.position);

        }
          
    }
}
