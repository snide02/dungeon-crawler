using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TestMoveScript : MonoBehaviour
{
    public Camera camera;
    public MovementBehavior movementBehavior;
    public GridOccupant gridOccupant;
    public TurnBasedObject turnBased;

    // Start is called before the first frame update
    void Start()
    {
       turnBased.OnStartTurn = OnTurnStart;
    }

    public void OnTurnStart() {

          Debug.Log("LLLLL test enemy start turn");
        Vector2Int startPos = gridOccupant.GetCenterCell();
        int maxSteps = 3;
        //MoveToMouse(startPos, maxSteps);
        MoveToPlayer(startPos, maxSteps);
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetMouseButtonDown(0)) {

            Debug.Log("LLLLL start turn");
            GameManager.TurnOrderManager.ExecuteTurns();


        }
          
    }

    void MoveToMouse(Vector2Int startPos, int steps) {
            Debug.Log("Click Pos " + startPos);   
            Vector3 worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
             Debug.Log("Target Pos" + target);
 
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, 3, cell => false);
          

            Vector3 finished = gridOccupant.GridToWorld(data.CellPosition);
             Debug.Log("Finished Coords" + finished);
            
            transform.position = finished;
             Debug.Log("Current Coords" + transform.position);
    }


    void MoveToPlayer(Vector2Int startPos, int steps) {
            Debug.Log("Click Pos " + startPos);   
            Vector3 worldPos = GameManager.Player.transform.position;
            
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
             Debug.Log("Target Pos" + target);
             ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();

             Debug.Log("Occupied cell counts is " + occupiedCells.Count );
             Debug.Log("GridOccupants counts is " + GameManager.GridOccupantManager.GridOccupants.Count );
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, 3, occupiedCells.Contains);
          

            Vector3 finished = gridOccupant.GridToWorld(data.CellPosition);
             Debug.Log("Finished Coords" + finished);
            
            transform.position = finished;
             Debug.Log("Current Coords" + transform.position);
    }

}
