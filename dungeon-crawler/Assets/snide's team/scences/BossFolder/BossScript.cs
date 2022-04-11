using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{//welcome to my class
    public Camera camera;
    public MovementBehavior movementBehavior;
    public GridOccupant gridOccupant;
    public TurnBasedObject turnBased;
    int tileSize;
    int bossX;
    int playerX;
    int playerHealth;

    bool playerInOne; //check if player in range for ground slam
    bool playerInTwo; //check if player in range for bite
    bool playerInSix; //check if player in range for web shot
    bool bossMove; //checks if boss needs to move towards player
    bool groundSlamCharging; //checks if ground slam is being chraged
    bool bossTurn; //checks if it is the boss' turn

    // Start is called before the first frame update
    void Start()
    {
        turnBased.OnStartTurn = OnTurnStart;
    }
public void OnTurnStart() {

          Debug.Log("LLLLL test enemy start turn");
        Vector2Int startPos = gridOccupant.GetCenterCell();
        int maxSteps = 1;
        MoveToPlayer(startPos, maxSteps);
    }
    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)) {

            Debug.Log("LLLLL start turn");
            GameManager.TurnOrderManager.ExecuteTurns();


        }
        if(bossTurn == true){
            playerPos();

            if(groundSlamCharging == true){
                //play ground slam attack animation
                if(playerInOne == true){
                    //damage player
                    playerHealth = playerHealth - 10;
                }
                playerInOne = false;
                groundSlamCharging = false;
            }

            if (groundSlamCharging == false){
                if(playerInOne == true){
                    //play ground slam charging animation
                    groundSlamCharging = true;
                }

                if(playerInTwo == true){
                    //play attack animation and add -10 health too player
                    //25% chance to add poison
                    playerHealth = playerHealth - 10;
                }

                if (playerInSix == true){
                    //play webshot animation

                }

                if (bossMove == true){
                    //move boss towards player
                    //bossX = bossX + tileSize;
                }
            }
        }
        
    }

 void MoveToPlayer(Vector2Int startPos, int maxSteps) {
            Debug.Log("Click Pos " + startPos);   
            Vector3 worldPos = GameManager.Player.transform.position;
            
            Vector2Int target = gridOccupant.WorldToGrid(worldPos);
             Debug.Log("Target Pos" + target);
             ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();

             Debug.Log("Occupied cell counts is " + occupiedCells.Count );
             Debug.Log("GridOccupants counts is " + GameManager.GridOccupantManager.GridOccupants.Count );
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, maxSteps, occupiedCells.Contains);
          

            Vector3 finished = gridOccupant.GridToWorld(data.FinalPosition);
             Debug.Log("Finished Coords" + finished);
            
            transform.position = finished;
             Debug.Log("Current Coords" + transform.position);
    }


    void playerPos(){ //check player position compared to boss
        playerInOne = false;
        playerInTwo = false;
        playerInSix = false;
        bossMove = false;

        if(playerX == bossX + 1){
            playerInOne = true;
            playerInTwo = false;
            playerInSix = false;
            bossMove = false;
        }
        if(playerX == bossX + 2){
            playerInOne = false;
            playerInTwo = true;
            playerInSix = false;
            bossMove = false;
        }
        if(playerX >= bossX + 3){
            playerInOne = false;
            playerInTwo = false;
            playerInSix = true;
            bossMove = false;
        }
        if(playerX >= bossX + 6){
            playerInOne = false;
            playerInTwo = false;
            playerInSix = false;
            bossMove = true;
        }
    }
}
