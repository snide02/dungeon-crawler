using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupant : MonoBehaviour
{

    public Grid WorldGrid;
    public Transform positionAnchor;

    public TransformToCell Transformer {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        Transformer = new TransformToSingleCell();
    }

    public Vector2Int WorldToGrid(Vector3 worldPos) {
        Vector3Int vecInt = WorldGrid.WorldToCell(worldPos);
        return new Vector2Int(vecInt.x, vecInt.y);
    }

    public Vector3 GridToWorld(Vector2Int vector2) {
        Vector3Int vec3 = new Vector3Int(vector2.x, vector2.y, 0);
        return WorldGrid.CellToWorld(vec3);
    }

    public Vector2Int[] getOccupiedCells() {
        return Transformer.Convert(WorldGrid, positionAnchor);
    }

    public static int ManhattanDistanceTo(Vector2Int startCell, Vector2Int target) {
        return (int)Mathf.Abs(target.y- startCell.y)  + Mathf.Abs(target.x - startCell.x);
    }

      public static int EuclideanDistanceSquareTo(Vector2Int startCell, Vector2Int target) {
        return (int)((target.y- startCell.y)* (target.y- startCell.y)  + (target.x - startCell.x)*(target.x - startCell.x));
    }


    public interface TransformToCell {

        Vector2Int[] Convert(Grid WorldGrid, Transform transform);
    }

    public class TransformToSingleCell : TransformToCell {

        public Vector2Int[] Convert(Grid WorldGrid, Transform transform) {
            Vector3 rawPosition = transform.position;
            Vector3Int result = WorldGrid.LocalToCell(new Vector3(rawPosition.x, rawPosition.y, 0.0f));
            return new Vector2Int[] { new Vector2Int(result.x, result.y)};
        }
    }

}
