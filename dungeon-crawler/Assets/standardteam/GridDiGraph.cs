using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Util
{
    public class GridDiGraph<N, E>
    {

        public int Width { get; }
        public int Height { get; }

        private IDictionary<int, Node> _nodes = new Dictionary<int, Node>();


        public GridDiGraph(int width, int height)
        {
            Width = width;
            Height = height;
        }



        private int convertVector(Vector2Int coordinates) {
            return coordinates.y * Width + coordinates.x % Width; 
        }


        public Node GetNode(Vector2Int coordinates) {
            return _nodes[convertVector(coordinates)];    
        }


        public Node AddNode(Vector2Int coordinates, N val)
        {
            return _nodes[convertVector(coordinates)] = new Node(this, coordinates, val);
        }

        public bool RemoveNode(Vector2Int coordinates)
        {
            return _nodes.Remove(convertVector(coordinates));
        }

        public bool ContainsNode(Vector2Int coordinates)
        {
            return _nodes.ContainsKey(convertVector(coordinates));
        }


        public ICollection<Node> GetNodes() {
            return _nodes.Values;
        }

        public class Node
        {

            public Vector2Int Coordinates { get; private set; }
            public N Value { get; set; }

            private GridDiGraph<N, E> _graph;
            private IDictionary<Direction, Edge> _edges = new Dictionary<Direction, Edge>();

            internal Node(GridDiGraph<N,E> graph, Vector2Int coordinates, N Val) {
                _graph = graph;
                Coordinates = coordinates;
                Value = Val;
            
            }


            public Edge GetEdge(Direction direction)
            {
                return _edges[direction] ;
            }


            public Edge AddEdge(Direction direction, E val)
            {
                return _edges[direction] = new Edge(_graph, Coordinates, Coordinates + convertDirection(direction), direction, val);
            }

            public bool RemoveNode(Direction direction)
            {
                return _edges.Remove(direction);
            }

            public bool ContainsNode(Direction direction)
            {
                return _edges.ContainsKey(direction);
            }

            public Vector2Int convertDirection(Direction direction)
            {
                return direction switch
                {
                    Direction.NORTH => new Vector2Int(0, 1),
                    Direction.SOUTH => new Vector2Int(0, -1),
                    Direction.EAST => new Vector2Int(1, 0),
                    Direction.WEST => new Vector2Int(-1, 0)
                };
            }

            public ICollection<Edge> GetEdges()
            {
                return _edges.Values;
            }
        }


        public class Edge
        {

            private GridDiGraph<N, E> _graph;
            private  Vector2Int _incidentFrom;
            private  Vector2Int _incidentTo;

            public Node IncidentFrom { get => _graph.GetNode(_incidentFrom); }
            public Node IncidentTo { get => _graph.GetNode(_incidentTo); }

            public Direction Direction { get; private set; }
            public E Value { get; set; }

            internal Edge(GridDiGraph<N, E> graph, Vector2Int from, Vector2Int to, Direction direction, E value)
            {
                _graph = graph;
                _incidentFrom = from;
                _incidentTo = to;
                Direction = direction;
                Value = value;
            }

        }


        public enum Direction
        {

            NORTH, WEST, EAST, SOUTH

          
        }
    }
}
