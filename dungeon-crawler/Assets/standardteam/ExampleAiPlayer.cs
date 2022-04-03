using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAiPlayer : MonoBehaviour
{

    public GridOccupant occupant;

    // Start is called before the first frame update
    void Start()
    {
       occupant =   GetComponent<GridOccupant>(); 
       GameManager.Player = gameObject;
       OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnEnable() {
        if (occupant != null) {
            GameManager.Player = gameObject;
            GameManager.GridOccupantManager.register(occupant);
        }
    }

    void OnDisable() {
        if (occupant != null) {
            GameManager.Player = null;
            GameManager.GridOccupantManager.unregister(occupant);
        }
    }




}
