using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupant : MonoBehaviour
{

    public Grid WorldGrid;
    public Transform positionAnchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int[] getOccupiedCells() {

        Vector3 rawPosition = positionAnchor.position;
        Vector3Int result = WorldGrid.LocalToCell(new Vector3(rawPosition.x, rawPosition.y, 0.0f));
        return new Vector2Int[] { new Vector2Int(result.x, result.y)};

    }

    public static int ManhattanDistanceTo(Vector2Int startCell, Vector2Int target) {
        return (int)Mathf.Abs(startCell.y - target.y)  + Mathf.Abs(target.x - startCell.y);
    }

}
