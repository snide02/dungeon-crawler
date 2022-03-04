using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationMaze : MonoBehaviour
{
    private Grid Grid;
    public GameObject wallPrefab;

    public int MazeRoomWidth;
    public int MazeRoomHeight;
    public int RoomWidth;
    public int RoomGap;


    // Start is called before the first frame update
    void Start()
    { 
        Grid = GetComponent<Grid>();

        for (int roomX = 0; roomX < MazeRoomWidth; roomX += 1)
        {
            for (int roomY = 0; roomY < MazeRoomHeight; roomY += 1)
            {

                int gridx = roomX * (RoomWidth+2) + roomX * RoomGap;
                int gridy = roomY * (RoomWidth+2) + roomY * RoomGap;

                printWalls(gridx, gridy);
            }
        }
    }

    public void printWalls(int gridx, int gridY) 
    {

        for (int x = 1; x <= RoomWidth;x +=1) 
        {

           GameObject top = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY, 0)), Quaternion.identity);
           GameObject bottom = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY + RoomWidth +1, 0)), Quaternion.identity);

        }


        for (int y = 1; y <= RoomWidth; y += 1)
        {

           GameObject left = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + y, 0)), Quaternion.identity);
           GameObject right = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth +1, gridY + y, 0)), Quaternion.identity);

        }

        GameObject topleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY, 0)), Quaternion.identity);
        GameObject topRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + RoomWidth+1, 0)), Quaternion.identity);
        GameObject bottomleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth+1, gridY, 0)), Quaternion.identity);
        GameObject bottomRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth+1, gridY + RoomWidth+1, 0)), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
