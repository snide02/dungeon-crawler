using System;
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
        randomGeneration();
    }


    public enum RoomType { 
        START,
        BOSS,
        EMPTY,
        EASY,
        MEDIUM,
        HARD,
        KEY_CHEST,
        ITEM_CHEST,

    
    }

    private RoomType RandomRoomType(int tier, int itemRooms) {

        if (tier == 0)
        {
            return RoomType.START;
        }
        else if (tier == 1)
        {
            return RoomType.BOSS;
        }
        else if (tier == 2)
        {
            return RoomType.KEY_CHEST;
        }
        else {
            int upperBound = 100;
            int easyWeight = 40; //100
            int mediumWeight = 30; //100
            int hardWeight = 20;

            int itemWeight = itemRooms switch {

                0 => 10,
                1=> 5,
                _ => 1
            };
            
            int sum = easyWeight + mediumWeight + hardWeight + itemWeight;
            int emptyWeight = upperBound - sum;

            if (easyWeight < 0) {
                throw new ArgumentException("Emty weight is negative " + emptyWeight);
            }


            int roll = UnityEngine.Random.Range(0, upperBound);
            RoomType? selected = null;

            if (selected == null && roll > upperBound - easyWeight) {
                selected = RoomType.EASY;
            }

            upperBound -= easyWeight;

            if (selected == null &&  roll > upperBound - mediumWeight)
            {
                selected = RoomType.MEDIUM;
            }

            upperBound -= hardWeight;

            if (selected == null && roll > upperBound - hardWeight)
            {
                selected = RoomType.HARD;
            }

            upperBound -= hardWeight;

            if (selected == null &&  roll > upperBound - itemWeight)
            {
                selected = RoomType.ITEM_CHEST;
            }

            upperBound -= itemWeight;

            return selected ?? RoomType.EMPTY;
        }
    }




    public void randomGeneration()
    {

        int tier = 0;
        int itemRooms = 0;
        Vector2Int?[] rooms = new Vector2Int?[MazeRoomHeight * MazeRoomWidth];
        int index = 0;


        for (int roomX = 0; roomX < MazeRoomWidth; roomX += 1)
        {
            for (int roomY = 0; roomY < MazeRoomHeight; roomY += 1)
            {
                rooms[index] = new Vector2Int(roomX, roomY);
                index += 1;
            }
        }

        Action<Vector2Int> placeRoom = (Vector2Int room) =>
        {
            int gridx = room.x * (RoomWidth + 2) + room.x * RoomGap;
            int gridy = room.y * (RoomWidth + 2) + room.y * RoomGap;

            RoomType roomType = RandomRoomType(tier, itemRooms);
            tier += 1;

            if (roomType == RoomType.ITEM_CHEST)
            {
                itemRooms += 1;
            }
            printWalls(gridx, gridy, roomType);
        };



        for (int times = 0; times < rooms.Length; times += 1) {


            int slot = UnityEngine.Random.Range(0, rooms.Length);
            for (int offset = 0; offset < rooms.Length; offset += 1) {

                int selected = (slot + offset) % rooms.Length;

                if (rooms[selected].HasValue) {

                    placeRoom(rooms[selected].Value);
                    rooms[selected] = null;
                    break;
                } 
            }
        }


    }

    public void plainGeneration() {

        int tier = 0;
        int itemRooms = 0;

        for (int roomX = 0; roomX < MazeRoomWidth; roomX += 1)
        {
            for (int roomY = 0; roomY < MazeRoomHeight; roomY += 1)
            {

                int gridx = roomX * (RoomWidth + 2) + roomX * RoomGap;
                int gridy = roomY * (RoomWidth + 2) + roomY * RoomGap;

                 RoomType roomType = RandomRoomType(tier, itemRooms);
                 tier += 1;

                if (roomType == RoomType.ITEM_CHEST) {
                    itemRooms += 1;
                }
                printWalls(gridx, gridy, roomType);
            }
        }
    }

    private void ColorWall(GameObject wall, RoomType type) {

        SpriteRenderer render = wall.GetComponentInChildren<SpriteRenderer>();
        Color color = Color.white;

        color = type switch {
            RoomType.START => Color.green,
            RoomType.BOSS => Color.black,
            RoomType.EMPTY => Color.white,
            RoomType.EASY => Color.cyan,
            RoomType.MEDIUM => new Color(0f, 0.5f, 1f),
            RoomType.HARD => Color.blue,
            RoomType.KEY_CHEST => Color.magenta,
            RoomType.ITEM_CHEST => new Color(1f, 0.95f, 0f),
            _ => Color.white

        };

        render.color = color;
    }

    public void printWalls(int gridx, int gridY, RoomType type) 
    {

        for (int x = 1; x <= RoomWidth;x +=1) 
        {

           GameObject top = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY, 0)), Quaternion.identity);
           GameObject bottom = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY + RoomWidth +1, 0)), Quaternion.identity);

           ColorWall(top, type);
           ColorWall(bottom, type);

        }


        for (int y = 1; y <= RoomWidth; y += 1)
        {

           GameObject left = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + y, 0)), Quaternion.identity);
           GameObject right = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth +1, gridY + y, 0)), Quaternion.identity);
           ColorWall(left, type);
           ColorWall(right, type);

        }

        GameObject topleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY, 0)), Quaternion.identity);
        GameObject topRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + RoomWidth+1, 0)), Quaternion.identity);
        GameObject bottomleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth+1, gridY, 0)), Quaternion.identity);
        GameObject bottomRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth+1, gridY + RoomWidth+1, 0)), Quaternion.identity);
        ColorWall(topleft, type);
        ColorWall(topRight, type);
        ColorWall(bottomleft, type);
        ColorWall(bottomRight, type);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
