using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupant : MonoBehaviour
{

    public Grid WorldGrid;
    public Transform positionAnchor;
    private bool registered;

    public TransformToCell Transformer {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        if (Transformer == null) {
            Transformer = new TransformToSingleCell();
        }
        OnEnable();
    }

    public Vector2Int WorldToGrid(Vector3 worldPos) {
        return GridOccupant.WorldToGrid(WorldGrid, worldPos);
    }

    public Vector3 GridToWorld(Vector2Int gridPos) {
        return GridOccupant.GridToWorld(WorldGrid, gridPos);
    }

    public Vector2Int[] GetOccupiedCells() {
        return Transformer.GetOccupiedCells(GetCenterCell());
    }

    public Vector2Int GetCenterCell() {
        return Transformer.GetCenterCell(WorldGrid, positionAnchor);
    }

    public static int ManhattanDistanceTo(Vector2Int startCell, Vector2Int target) {
        return (int)Mathf.Abs(target.y- startCell.y) + Mathf.Abs(target.x - startCell.x);
    }

      public static int EuclideanDistanceSquareTo(Vector2Int startCell, Vector2Int target) {
        return (int)((target.y- startCell.y) * (target.y- startCell.y)  + (target.x - startCell.x)*(target.x - startCell.x));
    }

    public static Vector2Int WorldToGrid(Grid WorldGrid, Vector3 worldPos) {
        Vector3Int vecInt = WorldGrid.WorldToCell(worldPos);
        return new Vector2Int(vecInt.x, vecInt.y);
    }

    public static Vector3 GridToWorld(Grid WorldGrid, Vector2Int gridPos) {
        Vector3Int vec3 = new Vector3Int(gridPos.x, gridPos.y, 0);
        return WorldGrid.CellToWorld(vec3);
    }


    public interface TransformToCell {

        Vector2Int[] GetOccupiedCells(Vector2Int centerCell);

        Vector2Int GetCenterCell(Grid WorldGrid, Transform transform);
    }

    public class TransformToSingleCell : TransformToCell {

        public Vector2Int[] GetOccupiedCells(Vector2Int centerCell) {
            return new Vector2Int[] {centerCell};
        }

        public Vector2Int GetCenterCell(Grid WorldGrid, Transform transform) {
            Vector3 rawPosition = transform.position;
            return GridOccupant.WorldToGrid(WorldGrid, new Vector3(rawPosition.x, rawPosition.y, 0.0f));
        }
    }

    void OnEnable() {
        if (GameManager.GridOccupantManager != null && !registered) {
            GameManager.GridOccupantManager.Register(this);
            registered = true;
        }
    }

    void OnDisable() {
        if (GameManager.GridOccupantManager != null && registered) {
            GameManager.GridOccupantManager.Unregister(this);
            registered = false;
        }
    }

}
