using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame { 
    public class GridOccupantManager : MonoBehaviour
    {
        [SerializeField]
        private ICollection<GridOccupant> objects = new List<GridOccupant>();
        [SerializeField]
        public ICollection<GridOccupant> GridOccupants {get => objects;}

        public void Register(GridOccupant obj) {
            objects.Add(obj);

        }

        public void Unregister(GridOccupant obj) {
            objects.Remove(obj);

        }

        public ISet<Vector2Int> GetObtructedCells() {

            ISet<Vector2Int> set = new HashSet<Vector2Int>();

            foreach (var obj in objects) {

                Vector2Int[] cells = obj.GetOccupiedCells();
                foreach (var cell in cells) {
                    set.Add(cell);
                }           
            }

            return set;
        }

        public ICollection<GridOccupant> GetOccupantsAt(Vector2Int cell) {

            IList<GridOccupant> set = new List<GridOccupant>();

            foreach (var obj in objects) {

                Vector2Int[] cells = obj.GetOccupiedCells();

                foreach (Vector2Int vec in cells) {

                    if (vec == cell) {
                        set.Add(obj);
                        break;
                    }
                }         
            }

            return set;
        }

    }
}
