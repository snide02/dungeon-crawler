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
       //gridOccupant.Transformer = new TransformToThreeCell();
    }

    public class TransformToThreeCell : GridOccupant.TransformToCell {

        public Vector2Int[] GetOccupiedCells(Vector2Int centerCell) {
            return new Vector2Int[] {
                
                                 centerCell+ new Vector2Int(-1,1), centerCell + new Vector2Int(0,1), centerCell+ new Vector2Int(1,1),
                                centerCell+ new Vector2Int(-1,0), centerCell + new Vector2Int(0,0), centerCell+ new Vector2Int(1,0),
                                centerCell+ new Vector2Int(-1,-1), centerCell + new Vector2Int(0,-1), centerCell+ new Vector2Int(1,-1)
             };
        }

        public Vector2Int GetCenterCell(Grid WorldGrid, Transform transform) {
            Vector3 rawPosition = transform.position;
            return GridOccupant.WorldToGrid(WorldGrid, new Vector3(rawPosition.x, rawPosition.y, 0.0f));
        }
    }

    public void OnTurnStart() {

        Vector2Int startPos = gridOccupant.GetCenterCell();
        int maxSteps = 1;

                    Debug.Log("Click Pos " + startPos);   
    
        MoveToPlayer(startPos, maxSteps);

            Debug.Log("end Pos " + startPos); 

          //MoveToMouse(startPos, maxSteps);
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetMouseButtonDown(0)) {

            Debug.Log("LLLLL start turn");
            GameManager.TurnOrderManager.ExecuteTurns();


        }
          
    }

    void MoveToMouse(Vector2Int startPos, int maxSteps) {
            Debug.Log("Click Pos " + startPos);   
            Vector3 worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
             Debug.Log("Target Pos" + target);
 
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, maxSteps, cell => false);
          

            Vector3 finished = gridOccupant.GridToWorld(data.FinalPosition);
             Debug.Log("Finished Coords" + finished);
            
            transform.position = finished;
             Debug.Log("Current Coords" + transform.position);
    }


    void MoveToPlayer(Vector2Int startPos, int maxSteps) {
            Vector3 worldPos = GameManager.Player.transform.position;
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
            ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();
            Predicate<Vector2Int> occipiedCellDetector = occupiedCells.Contains;
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, maxSteps, occipiedCellDetector);
            Vector3 finished = gridOccupant.GridToWorld(data.FinalPosition);
            transform.position = finished;

    }

}
