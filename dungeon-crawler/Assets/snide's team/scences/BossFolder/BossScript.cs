using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossScript : MonoBehaviour
{//welcome to my class smkde
    public Camera camera;
    public MovementBehavior movementBehavior;
    public GridOccupant gridOccupant;
    public TurnBasedObject turnBased;
    private SpriteRenderer renderer;
    int playerHealth;
    Vector3 bossPos;
    Vector3 playerPos;
    bool playerInOne; //check if player in range for ground slam
    bool playerInTwo; //check if player in range for bite
    bool playerInSix; //check if player in range for web shot
    bool bossMove = false; //checks if boss needs to move towards player
    bool groundSlamCharging = false; //checks if ground slam is being chraged
    bool bossAttack = false; //checks if it is the boss' turn

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        turnBased.OnStartTurn = OnTurnStart;
        gridOccupant.Transformer = new TransformToThreeCell();
    }
      public class TransformToThreeCell : GridOccupant.TransformToCell {

        public Vector2Int[] GetOccupiedCells(Vector2Int centerCell) {
            return new Vector2Int[] {
                        centerCell + new Vector2Int(0,1), centerCell+ new Vector2Int(1,1),
                        centerCell + new Vector2Int(0,0), centerCell+ new Vector2Int(1,0)
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
        if(bossMove){
            MoveToPlayer(startPos, maxSteps);
        } else{
            bossAttack = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        compare();
        playerPos = GameManager.Player.transform.position; //test player's postion
        bossPos = transform.position; //Enemy's position
        playerHealth = GameManager.Player.GetComponent<player>().healthPoints;
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("LLLLL start turn");
            GameManager.TurnOrderManager.ExecuteTurns();
        }
        if (playerPos.x > bossPos.x){
            renderer.flipX = true;
        } else{
            renderer.flipX = false;
        }
        if(bossAttack == true);{
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
                    GameManager.Player.GetComponent<player>().isWebbed = true;
                    //accesses the Player Object from the GameManager, then through the player script it accesses the isWebbed boolean.
                }
            }
            bossAttack = false;
        }
        
        //Debug.Log("Player Position" + playerPos);
        //Debug.Log("Boss Position" + bossPos);
        //Debug.Log("bossMove is" + bossMove);
        //Debug.Log("isWebbed= " +  GameManager.Player.GetComponent<player>().isWebbed);
    }

 void MoveToPlayer(Vector2Int startPos, int maxSteps) {
            Vector3 worldPos = GameManager.Player.transform.position; //test player's postion
            Vector2Int target = gridOccupant.WorldToGrid(worldPos); //where enemy is trying to get too
            ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();
            Predicate<Vector2Int> occipiedCellDetector = occupiedCells.Contains;
            MovementBehavior.MovementData data =  movementBehavior.calculateMoveToTarget(startPos, target, maxSteps, occipiedCellDetector);
            Vector3 finished = gridOccupant.GridToWorld(data.FinalPosition); //calculates the psotion to move too
            transform.position = finished; //moves the enemy
    }


    void compare(){ //check player position compared to boss
        playerInOne = false;
        playerInTwo = false;
        playerInSix = false;
        bossMove = false;

        float distance = Math.Abs(playerPos.y) - Math.Abs(bossPos.y) + Math.Abs(playerPos.x) - Math.Abs(bossPos.x);

        if(1 == distance){
            playerInOne = true;
            playerInTwo = false;
            playerInSix = false;
            bossMove = false;
        }
        if(2 == distance){
            playerInOne = false;
            playerInTwo = true;
            playerInSix = false;
            bossMove = false;
        }
        if(3 <= distance){
            playerInOne = false;
            playerInTwo = false;
            playerInSix = true;
            bossMove = false;
        }
        if(6 <= distance){
            playerInOne = false;
            playerInTwo = false;
            playerInSix = false;
            bossMove = true;
        }
    }
}
