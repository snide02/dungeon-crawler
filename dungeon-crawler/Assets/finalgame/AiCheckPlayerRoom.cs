using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DungeonGame { 
    public class AiCheckPlayerRoom : MonoBehaviour
    {
        private GridOccupant occupant;
        // Start is called before the first frame update
        void Start()
        {
            occupant = GetComponent<GridOccupant>();
            
        }



        public bool IsInSameRoomAsPlayer() {
            RoomGeneration room = GameManager.RoomGenerator;
            GridOccupant player = GameManager.Player.GetComponent<GridOccupant>();

            Vector2Int playerPos  = player.GetCenterCell();
            (int px, int py) = room.convertGridToRoom(playerPos.x, playerPos.y);

            Vector2Int mobPos  = occupant.GetCenterCell();
            (int mx, int my) = room.convertGridToRoom(mobPos.x, mobPos.y);

            if (px == mx || py == my) {
                Debug.Log($" {name} Player room {px},{py} and mob at {mx},{my}");
            }

            return px == mx && py == my;

        }
    }
}
