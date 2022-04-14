using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWallScript : MonoBehaviour
{
    private GridOccupant gridOccupant;
    public int RoomWidth;
    public GameObject wallPrefab;


    // Start is called before the first frame update
    void Start()
    {
        gridOccupant = GetComponent<GridOccupant>();
        TransformToWallCell cell = new TransformToWallCell();
        cell.roomWall = this;
        gridOccupant.Transformer = cell;
        Debug.Log("Room to wall walls " + gridOccupant.GetOccupiedCells().Length );



        foreach(Vector2Int wall in gridOccupant.GetOccupiedCells()) {

            Instantiate(wallPrefab, gridOccupant.GridToWorld(wall), Quaternion.identity);

        }

    }


    private class TransformToWallCell : GridOccupant.TransformToCell {
            
        public RoomWallScript roomWall {get; set;}

        public Vector2Int[] GetOccupiedCells(Vector2Int centerCell) {
        
        int totalArraySize =  4 * roomWall.RoomWidth + 4;
        Debug.Log("Room to wall totalArraySize " + totalArraySize );
        Vector2Int[] wallArray = new Vector2Int[totalArraySize];
        int index = 0;



        for (int x = 0; x <= roomWall.RoomWidth+1; x += 1)
        {

            Vector2Int offsetTop = new Vector2Int(x, 0);
            wallArray[index] = centerCell + offsetTop;
            index +=1;
            Vector2Int offsetBottom = new Vector2Int(x, roomWall.RoomWidth + 1);
            wallArray[index] = centerCell + offsetBottom;
            index+=1;

        }


        for (int y = 1; y <= roomWall.RoomWidth; y += 1)
        {

            Vector2Int offsetLeft = new Vector2Int(0, y);
            wallArray[index] = centerCell + offsetLeft;
            index +=1;
            Vector2Int offsetRight = new Vector2Int(roomWall.RoomWidth + 1, y);
            wallArray[index] = centerCell + offsetRight;
            index+=1;


        }

        return wallArray;

    }

        public Vector2Int GetCenterCell(Grid WorldGrid, Transform transform) {
            Vector3 rawPosition = transform.position;
            return GridOccupant.WorldToGrid(WorldGrid, new Vector3(rawPosition.x, rawPosition.y, 0.0f));
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
