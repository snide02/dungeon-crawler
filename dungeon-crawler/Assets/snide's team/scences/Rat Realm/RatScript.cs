using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Needs: Player functionality, poison effect on player
public class RatScript : MonoBehaviour
{
    public Camera camera;
    public MovementBehavior movementBehavior;
    public GridOccupant gridOccupant;
    public TurnBasedObject turnBased;

    Vector3 ratPos;
    Vector3 playerPos;
    int playerHealth;

    bool playerInRange;
    bool ratMove;
    bool ratAttack;

    // Start is called before the first frame update
    void Start()
    {
        turnBased.OnStartTurn = OnTurnStart;
    }

    public void OnTurnStart()
    {
        Debug.Log("On Turn Start - Rat");
        Vector2Int startPos = gridOccupant.GetCenterCell();
        int maxSteps = 1;
        if (ratMove)
        {
            MoveToPlayer(startPos, maxSteps);
        }
        else
        {
            ratAttack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        compare();
        playerPos = GameManager.Player.transform.position; //test player's postion
        ratPos = transform.position; //Enemy's position aka Rat location
        playerHealth = GameManager.Player.GetComponent<player>().healthPoints;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LLLLL start turn");
            GameManager.TurnOrderManager.ExecuteTurns();
        }
        if (ratAttack)
        {
            if (playerInRange)
            {
                //attack animation
                playerHealth -= 2;
                usePoison(); // calulates if poison is used, true if poison
            }
            ratAttack = false;
        }
    }

    bool usePoison()
    {
        if (UnityEngine.Random.Range(0, 3) == 1)
        {
            return true;
        }
        return false;
    }

    void MoveToPlayer(Vector2Int startPos, int maxSteps)
    {
        Vector3 worldPos = GameManager.Player.transform.position; //test player's postion
        Vector2Int target = gridOccupant.WorldToGrid(worldPos); //where enemy is trying to get too
        ISet<Vector2Int> occupiedCells = GameManager.GridOccupantManager.GetObtructedCells();
        Predicate<Vector2Int> occipiedCellDetector = occupiedCells.Contains;
        MovementBehavior.MovementData data = movementBehavior.calculateMoveToTarget(startPos, target, maxSteps, occipiedCellDetector);
        Vector3 finished = gridOccupant.GridToWorld(data.FinalPosition); //calculates the psotion to move too
        transform.position = finished; //moves the enemy
    }

    void compare()
    { //check player position compared to boss
        playerInRange = false;
        ratMove = false;

        float distance = Math.Abs(playerPos.y) - Math.Abs(ratPos.y) + Math.Abs(playerPos.x) - Math.Abs(ratPos.x);

        if (1 >= distance)
        {
            playerInRange = true;
            ratMove = false;
        }
        else if (1 < distance) {
            playerInRange = false;
            ratMove = true;

        }
    }
}

