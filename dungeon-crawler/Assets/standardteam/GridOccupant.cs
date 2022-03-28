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


    public Vector2Int WorldToGrid(Vector3 worldPos) {
        Vector3Int vecInt = WorldGrid.WorldToCell(worldPos);
        return new Vector2Int(vecInt.x, vecInt.y);
    }

    public Vector3 GridToWorld(Vector2Int vector2) {
        Vector3Int vec3 = new Vector3Int(vector2.x, vector2.y, 0);
        return WorldGrid.CellToWorld(vec3);
    }

    public virtual Vector2Int[] getOccupiedCells() {

        Vector3 rawPosition = positionAnchor.position;
        Vector3Int result = WorldGrid.LocalToCell(new Vector3(rawPosition.x, rawPosition.y, 0.0f));
        return new Vector2Int[] { new Vector2Int(result.x, result.y)};
    }

    public static int ManhattanDistanceTo(Vector2Int startCell, Vector2Int target) {
        return (int)Mathf.Abs(target.y- startCell.y)  + Mathf.Abs(target.x - startCell.x);
    }

      public static int EuclideanDistanceSquareTo(Vector2Int startCell, Vector2Int target) {
        return (int)((target.y- startCell.y)* (target.y- startCell.y)  + (target.x - startCell.x)*(target.x - startCell.x));
    }

}
