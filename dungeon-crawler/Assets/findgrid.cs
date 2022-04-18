using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findgrid : MonoBehaviour
{

    public GameObject Grid;
    public Grid griddy;
    public GridOccupant placeholder;
    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.Find("GridObject");
        griddy = Grid.GetComponent<Grid>();
        placeholder = this.GetComponent<GridOccupant>();
        placeholder.WorldGrid = griddy; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
