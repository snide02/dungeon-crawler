using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomGeneration : MonoBehaviour
{
    private Grid Grid;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public Transform player;
    public GameObject Mummy;
    public GameObject thief;
    public GameObject boss;
    public GameObject stairs;
    public GameObject chest;
    public GameObject daboss;

    public int MazeRoomWidth;
    public int MazeRoomHeight;
    public int RoomWidth;
    public int RoomGap;
    IDictionary<Vector2Int, List<Vector2Int>> roomAdjacencyList;

    // Start is called before the first frame update
    void Start()
    {
        Grid = GetComponent<Grid>();
        var settings = randomGeneration();
        roomAdjacencyList = doorGeneration(settings);

        var nullCheck = roomAdjacencyList.Keys;
        placeDoors(roomAdjacencyList);
        teleportPlayer(settings, player);
    }


    public enum RoomType
    {
        START,
        BOSS,
        EMPTY,
        EASY,
        MEDIUM,
        HARD,
        KEY_CHEST,
        ITEM_CHEST,


    }
    GameObject bosschest;
    bool check = true;
    private void Update()
    {

        if(!daboss.activeInHierarchy && check)
        {
            check = false;
            int thex = (int) daboss.transform.position.x;
            int they = (int)daboss.transform.position.y;

            int thez = (int)daboss.transform.position.z;



            bosschest = Instantiate(chest, Grid.CellToLocal(new Vector3Int(thex, they, thez)), Quaternion.identity);


            Instantiate(stairs, Grid.CellToLocal(new Vector3Int(thex, they+1, thez)), Quaternion.identity);
            

        }


    }

    private RoomType RandomRoomType(int tier, int itemRooms)
    {

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
        else
        {
            int upperBound = 100;
            int easyWeight = 40; //100
            int mediumWeight = 30; //100
            int hardWeight = 20;

            int itemWeight = itemRooms switch
            {

                0 => 10,
                1 => 5,
                _ => 1
            };

            int sum = easyWeight + mediumWeight + hardWeight + itemWeight;
            int emptyWeight = upperBound - sum;

            if (easyWeight < 0)
            {
                throw new ArgumentException("Emty weight is negative " + emptyWeight);
            }


            int roll = UnityEngine.Random.Range(0, upperBound);
            RoomType? selected = null;

            if (selected == null && roll > upperBound - easyWeight)
            {
                selected = RoomType.EASY;
            }

            upperBound -= easyWeight;

            if (selected == null && roll > upperBound - mediumWeight)
            {
                selected = RoomType.MEDIUM;
            }

            upperBound -= hardWeight;

            if (selected == null && roll > upperBound - hardWeight)
            {
                selected = RoomType.HARD;
            }

            upperBound -= hardWeight;

            if (selected == null && roll > upperBound - itemWeight)
            {
                selected = RoomType.ITEM_CHEST;
            }

            upperBound -= itemWeight;

            return selected ?? RoomType.EMPTY;
        }
    }



    (int, int) convertRoomToGrid(int roomX, int roomY)
    {
        int gridx = roomX * (RoomWidth + 2) + roomX * RoomGap;
        int gridy = roomY * (RoomWidth + 2) + roomY * RoomGap;

        return (gridx, gridy);
    }


    class DoorGenerationSettings
    {

        public Vector2Int StartRoom { get; set; }
        public List<Vector2Int> ForceDeadIns { get; }

        public DoorGenerationSettings(Vector2Int start)
        {
            StartRoom = start;
            ForceDeadIns = new List<Vector2Int>();
        }
    }

    DoorGenerationSettings randomGeneration()
    {
        var settings = new DoorGenerationSettings(new Vector2Int(-1, -1));


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

        for (int times = 0; times < rooms.Length; times += 1)
        {


            int slot = UnityEngine.Random.Range(0, rooms.Length);
            for (int offset = 0; offset < rooms.Length; offset += 1)
            {

                int selected = (slot + offset) % rooms.Length;

                if (rooms[selected].HasValue)
                {

                    Vector2Int room = rooms[selected].Value;

                    var (gridx, gridy) = convertRoomToGrid(room.x, room.y);

                    RoomType roomType = RandomRoomType(tier, itemRooms);
                    tier += 1;


                    if (roomType == RoomType.START)
                    {

                        settings.StartRoom = new Vector2Int(room.x, room.y);
                    }


                    if (roomType == RoomType.BOSS || roomType == RoomType.KEY_CHEST)
                    {

                        settings.ForceDeadIns.Add(new Vector2Int(room.x, room.y));
                    }

                    if (roomType == RoomType.ITEM_CHEST)
                    {
                        itemRooms += 1;
                    }
                    printWalls(gridx, gridy, roomType);
                    rooms[selected] = null;
                    break;
                }
            }
        }

        return settings;
    }

    public void plainGeneration()
    {

        int tier = 0;
        int itemRooms = 0;

        for (int roomX = 0; roomX < MazeRoomWidth; roomX += 1)
        {
            for (int roomY = 0; roomY < MazeRoomHeight; roomY += 1)
            {

                var (gridx, gridy) = convertRoomToGrid(roomX, roomY);

                RoomType roomType = RandomRoomType(tier, itemRooms);
                tier += 1;

                if (roomType == RoomType.ITEM_CHEST)
                {
                    itemRooms += 1;
                }
                printWalls(gridx, gridy, roomType);
            }
        }
    }

    private void ColorWall(GameObject wall, RoomType type)
    {

        SpriteRenderer render = wall.GetComponentInChildren<SpriteRenderer>();
        Color color = Color.white;

        color = type switch
        {
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

        for (int x = 1; x <= RoomWidth; x += 1)
        {

            GameObject top = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY, 0)), Quaternion.identity);
            GameObject bottom = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + x, gridY + RoomWidth + 1, 0)), Quaternion.identity);

            ColorWall(top, type);
            ColorWall(bottom, type);

        }


        for (int y = 1; y <= RoomWidth; y += 1)
        {

            GameObject left = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + y, 0)), Quaternion.identity);
            GameObject right = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth + 1, gridY + y, 0)), Quaternion.identity);
            ColorWall(left, type);
            ColorWall(right, type);

        }

        GameObject topleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY, 0)), Quaternion.identity);//bottom left


        if(type == RoomType.EASY)
        {
            Instantiate(Mummy, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);

        }

        if (type == RoomType.MEDIUM)
        {
            Instantiate(thief, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);
            Instantiate(thief, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);

        }

        if (type == RoomType.HARD)
        {
            Instantiate(thief, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);
            Instantiate(thief, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);
            Instantiate(Mummy, Grid.CellToLocal(new Vector3Int(gridx + (Random.Range(1, RoomWidth)), gridY + (Random.Range(1, RoomWidth)), 0)), Quaternion.identity);

        }

        if (type == RoomType.BOSS)
        {
            daboss = Instantiate(boss, Grid.CellToLocal(new Vector3Int(gridx +1+ (Random.Range(RoomWidth/2, RoomWidth/2)), gridY +1+ (Random.Range(RoomWidth / 2, RoomWidth / 2)), 0)), Quaternion.identity);


        }

        

        GameObject topRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx, gridY + RoomWidth + 1, 0)), Quaternion.identity);
        GameObject bottomleft = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth + 1, gridY, 0)), Quaternion.identity);
        GameObject bottomRight = Instantiate(wallPrefab, Grid.CellToLocal(new Vector3Int(gridx + RoomWidth + 1, gridY + RoomWidth + 1, 0)), Quaternion.identity);
        ColorWall(topleft, type);
        ColorWall(topRight, type);
        ColorWall(bottomleft, type);
        ColorWall(bottomRight, type);


    }

    struct Edge
    {
        public Vector2Int room { get; set; }
        public Direction direction { get; set; }
    }

    enum Direction
    {
        NORTH, SOUTH, EAST, WEST, SELF
    }


    Vector2Int DirectionToOffset(Direction direction)
    {
        return direction switch
        {
            Direction.EAST => new Vector2Int(1, 0),
            Direction.WEST => new Vector2Int(-1, 0),
            Direction.NORTH => new Vector2Int(0, 1),
            Direction.SOUTH => new Vector2Int(0, -1),
            _ => new Vector2Int(0, 0)
        };
    }


    bool isValidPoint(Vector2Int point)
    {

        return 0 <= point.x && point.x < MazeRoomWidth
            && 0 <= point.y && point.y < MazeRoomHeight;

    }

    IDictionary<Vector2Int, List<Vector2Int>> doorGeneration(DoorGenerationSettings settings)
    {
        IDictionary<Vector2Int, List<Vector2Int>> adjacencyList = new Dictionary<Vector2Int, List<Vector2Int>>();
        bool[,] visited = new bool[MazeRoomWidth, MazeRoomHeight];
        IList<Edge> adjacentRooms = new List<Edge>();
        adjacentRooms.Add(new Edge() { room = settings.StartRoom, direction = Direction.SELF });

        while (adjacentRooms.Count != 0)
        {
            int selected = UnityEngine.Random.Range(0, adjacentRooms.Count);
            Edge edge = adjacentRooms[selected];
            adjacentRooms.RemoveAt(selected);

            if (visited[edge.room.x, edge.room.y])
            {
                continue;
            }

            //Link rooms to parent rooms
            if (edge.direction != Direction.SELF)
            {

                Vector2Int pastPosition = edge.room + DirectionToOffset(edge.direction);

                adjacencyList[pastPosition].Add(edge.room);

                var list = new List<Vector2Int>();
                list.Add(pastPosition);
                adjacencyList[edge.room] = list;
            }
            else
            {
                adjacencyList[edge.room] = new List<Vector2Int>();
            }

            visited[edge.room.x, edge.room.y] = true;


            if (settings.ForceDeadIns.Contains(edge.room))
            {
                continue;
            }

            //Add neighbors and check that they are not visited

            Vector2Int north = edge.room + DirectionToOffset(Direction.NORTH);
            Vector2Int south = edge.room + DirectionToOffset(Direction.SOUTH);
            Vector2Int west = edge.room + DirectionToOffset(Direction.WEST);
            Vector2Int east = edge.room + DirectionToOffset(Direction.EAST);

            if (isValidPoint(north) && !visited[north.x, north.y])
            {
                adjacentRooms.Add(new Edge() { room = north, direction = Direction.SOUTH });
            }

            if (isValidPoint(south) && !visited[south.x, south.y])
            {
                adjacentRooms.Add(new Edge() { room = south, direction = Direction.NORTH });
            }

            if (isValidPoint(west) && !visited[west.x, west.y])
            {
                adjacentRooms.Add(new Edge() { room = west, direction = Direction.EAST });
            }

            if (isValidPoint(east) && !visited[east.x, east.y])
            {
                adjacentRooms.Add(new Edge() { room = east, direction = Direction.WEST });
            }
        }

        return adjacencyList;
    }


    public void placeDoors(IDictionary<Vector2Int, List<Vector2Int>> dictionary)
    {

        foreach (Vector2Int room in dictionary.Keys)
        {

            List<Vector2Int> adjacent = dictionary[room];

            //Get grid coordinates for the room
            //Find the direction of Adjecent rooms
            //Place door objects in the grid space based on adjacent room direction


            (int gridx, int gridy) = convertRoomToGrid(room.x, room.y);

            int radius = 1 + RoomWidth / 2;
            int centerx = gridx + radius;
            int centery = gridy + radius;
            /// 

            foreach (Vector2Int adjacentRoom in adjacent)
            {

                Vector2Int offset = adjacentRoom - room;


                int doorx = centerx;
                int doory = centery;


                doorx += offset.x * (radius);
                doory += offset.y * (radius);


                Vector3 doorWorldCoord = Grid.CellToLocal(new Vector3Int(doorx, doory, 0));
                doorWorldCoord.z = -1;

                GameObject door = Instantiate(doorPrefab, doorWorldCoord, Quaternion.identity);
                DoorTeleportDestination destination = door.GetComponent<DoorTeleportDestination>();
                destination.teleportDestination = doorWorldCoord + (RoomGap + 2) * new Vector3(offset.x, offset.y, 0);
                destination.teleportDestination.z = 0;
            }
        }

    }


    public void DrawPaths(IDictionary<Vector2Int, List<Vector2Int>> dictionary)
    {
        foreach (Vector2Int room in dictionary.Keys)
        {

            List<Vector2Int> adjacent = dictionary[room];

            //Get grid coordinates for the room
            //Find the direction of Adjecent rooms
            //Place door objects in the grid space based on adjacent room direction


            (int gridx, int gridy) = convertRoomToGrid(room.x, room.y);

            int radius = 1 + RoomWidth / 2;
            int centerx = gridx + radius;
            int centery = gridy + radius;
            /// 

            foreach (Vector2Int adjacentRoom in adjacent)
            {

                Vector2Int offset = adjacentRoom - room;


                int doorx = centerx;
                int doory = centery;


                doorx += offset.x * (radius);
                doory += offset.y * (radius);


                Vector3 doorWorldCoord = Grid.CellToLocal(new Vector3Int(doorx, doory, 0));
                doorWorldCoord.z = -2;


                Vector3 destination = doorWorldCoord + (RoomGap + 1) * new Vector3(offset.x, offset.y, -2);
                Gizmos.DrawLine(doorWorldCoord, destination);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        DrawPaths(roomAdjacencyList);
    }


    void teleportPlayer(DoorGenerationSettings settings, Transform player)
    {


        (int gridx, int gridy) = convertRoomToGrid(settings.StartRoom.x, settings.StartRoom.y);

        int radius = 1 + RoomWidth / 2;
        int centerx = gridx + radius;
        int centery = gridy + radius;


        Vector3 teleportLocation = Grid.CellToLocal(new Vector3Int(centerx, centery, 0));


        player.position = teleportLocation;
    }

}





