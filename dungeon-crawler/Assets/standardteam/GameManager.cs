using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Player {get; set;}
    public static TurnOrderManager TurnOrderManager {get; set;}
    public static GridOccupantManager GridOccupantManager {get; set;}
    public static Vector2Int? CurrentPlayerRoom {get; set;}

    void Start() {
       
        Debug.Log("GameManager started");


    }

    void OnEnable() {
        GameManager.TurnOrderManager = GetComponent<TurnOrderManager>();
        GameManager.GridOccupantManager = GetComponent<GridOccupantManager>();
        Debug.Log("GameManager enabled");
    }
}
