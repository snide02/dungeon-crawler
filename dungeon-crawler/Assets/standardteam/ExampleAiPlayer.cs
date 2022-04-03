using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAiPlayer : MonoBehaviour
{

    public GridOccupant occupant;

    // Start is called before the first frame update
    void Start()
    {
       occupant = GetComponent<GridOccupant>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnEnable() {
        GameManager.Player = gameObject;
    }

    void OnDisable() {
        GameManager.Player = null;
    }




}
