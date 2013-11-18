using PathFinderCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderAlgorithms
{
    public class AStarRouting
    {

#region "Private Variables"

        private int _DIAGONAL_MOVEMENT_COST = 12;
        private int _LINEAR_MOVEMENT_COST = 10;

        private int _mapHeight;
        private int _mapWidth;
        Point _origin;
        Point _destination;
        private List<Node> _openList = new List<Node>(20);
        private List<Node> _closedList = new List<Node>(20);
        private Node[,] _map;
        private bool _destinationFound = false;

#endregion

#region "Public Methods"

        public Point[] FindRoute(PathingMap pathingMap)
        {
            //init class variables
            _mapHeight = pathingMap.Stage.Height;
            _mapWidth = pathingMap.Stage.Width;
            _origin = pathingMap.Origin;
            _destination = pathingMap.Destination;
            

            _openList.Clear();
            _closedList.Clear();

            //Create Map
            _map = new Node[_mapWidth, _mapHeight];

            //Create Origin Node
            _map[_origin.x, _origin.y] = new Node(_origin.x, _origin.y, _destination.x, _destination.y);


            MarkObstaclesAsInaccessible(pathingMap.Obstacles);

            //Setup Open List to begin node scan
            Node[] _adjacentsNodes = GetAdjacentNodes(_map[_origin.x, _origin.y]);
            _closedList.Add(_map[_origin.x, _origin.y]);

            for (int i = 0; i < _adjacentsNodes.Length; i++)
            {
                _openList.Add(_adjacentsNodes[i]);

                //set surounding nodes to be children of Origin
                _adjacentsNodes[i].Parent = _map[_origin.x, _origin.y];
            }

            //Hi Ho, to node scanning we go
            while (!_destinationFound)
            {
                var currentNode = GetNodeWithSmallesCost();
                AddToClosedList(currentNode);

                _adjacentsNodes = GetAdjacentNodes(currentNode);

                for (int i = 0; i < _adjacentsNodes.Length; i++)
                {
                    if (!ContaintedInList( _adjacentsNodes[i], _closedList))
                    {
                        //If the node already exists see if its more effcient to travese from the current Node
                        if (_adjacentsNodes[i].PreviouslyCreated)
                        {
                            if ((currentNode.MovementCost + _adjacentsNodes[i].MovementCostFromCurrentAdjacent) < _adjacentsNodes[i].MovementCost)
                                _adjacentsNodes[i].Parent = currentNode;
                        }
                        else
                        {
                            _adjacentsNodes[i].Parent = currentNode;
                            _adjacentsNodes[i].MovementCost = currentNode.MovementCost + _adjacentsNodes[i].MovementCostFromCurrentAdjacent;

                            if (!ContaintedInList(_adjacentsNodes[i], _openList))
                                _openList.Add(_adjacentsNodes[i]);
                        }

                        //check to make sure we havent arrived at destination
                        if (_adjacentsNodes[i].Heuristic < 3)
                        {
                            if (IsAjacentToDestination(_adjacentsNodes[i]))
                            {
                                _destinationFound = true;
                            }
                        }
                    }
                }
            }

            Node finalNode = GetNodeWithLowestCost(_adjacentsNodes);
            return GetPath(finalNode);
        }



#endregion

#region "private Methods"

            //TODO: needs to be broken up if possible, lotsa of dup code.
            private Node[] GetAdjacentNodes(Node centerNode)
            {
                int currentX, currentY;
                List<Node> nodes = new List<Node>(7);

                //Top Left Node
                currentX = centerNode.Location.x - 1;
                currentY = centerNode.Location.y - 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _DIAGONAL_MOVEMENT_COST;

                        nodes.Add( _map[currentX, currentY]);
                    }
                }

                //Top Node
                currentX = centerNode.Location.x;
                currentY = centerNode.Location.y - 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _LINEAR_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }


                //Top Right Node
                currentX = centerNode.Location.x + 1;
                currentY = centerNode.Location.y - 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _DIAGONAL_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                //Right Node
                currentX = centerNode.Location.x + 1;
                currentY = centerNode.Location.y;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _LINEAR_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                //Bottom Right Node
                currentX = centerNode.Location.x + 1;
                currentY = centerNode.Location.y + 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _DIAGONAL_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                //Bottom
                currentX = centerNode.Location.x;
                currentY = centerNode.Location.y + 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _LINEAR_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                //Bottom Left
                currentX = centerNode.Location.x - 1;
                currentY = centerNode.Location.y + 1;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _DIAGONAL_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                //Left
                currentX = centerNode.Location.x - 1;
                currentY = centerNode.Location.y;

                if (IsInMapBounds(currentX, currentY))
                {
                    _map[currentX, currentY] = GetSingleAdjacentNode(currentX, currentY);

                    if (!_map[currentX, currentY].IsInaccessible)
                    {
                        _map[currentX, currentY].MovementCostFromCurrentAdjacent = _LINEAR_MOVEMENT_COST;
                        nodes.Add(_map[currentX, currentY]);
                    }
                }

                return nodes.ToArray();
            }

            private Node GetNodeWithSmallesCost()
            {
                var smallestCurrentNode = _openList[0];

                foreach (Node node in _openList)
                {
                    if (node.TotalCost < smallestCurrentNode.TotalCost)
                        smallestCurrentNode = node;
                }

                return smallestCurrentNode;
            }

            private void AddToClosedList(Node node)
            {
                _openList.Remove(node);
                _closedList.Add(node);
            }

            private bool IsInMapBounds(int x, int y)
            {
                if (x > 0 && y > 0)
                {
                    if (x < _mapWidth && y < _mapHeight)
                        return true;
                }
                    
                return false;
            }

            private Node GetSingleAdjacentNode(int x, int y)
            {
                if (_map[x, y] == null)
                {
                    _map[x, y] = new Node(x, y, _destination.x, _destination.y);
                    _map[x, y].PreviouslyCreated = false;
                }
                else
                {
                    _map[x, y].PreviouslyCreated = true;
                }

                return _map[x, y];
            }

            private bool IsAjacentToDestination(Node node)
            {
                var currentLocation = node.Location;

                if ((currentLocation.x - 1) == _destination.x && currentLocation.y == _destination.y)
                    return true;
                if ((currentLocation.x + 1) == _destination.x && currentLocation.y == _destination.y)
                    return true;
                if (currentLocation.x == _destination.x && (currentLocation.y - 1 == _destination.y))
                    return true;
                if (currentLocation.x == _destination.x && (currentLocation.y + 1 == _destination.y))
                    return true;
                if ((currentLocation.x + 1) == _destination.x && (currentLocation.y + 1 == _destination.y))
                    return true;
                if ((currentLocation.x - 1) == _destination.x && (currentLocation.y - 1 == _destination.y))
                    return true;
                if ((currentLocation.x + 1) == _destination.x && (currentLocation.y - 1 == _destination.y))
                    return true;
                if ((currentLocation.x - 1) == _destination.x && (currentLocation.y + 1 == _destination.y))
                    return true;

                return false;
            }

            private Point[] GetPath(Node FinalNode)
            {
                List<Point> path = new List<Point>(20);
                path.Add(_destination);

                while (true)
                {
                    if(FinalNode == null)
                        return path.ToArray();

                    path.Add(FinalNode.Location);
                    FinalNode = FinalNode.Parent;
                }
            }

            private bool ContaintedInList(Node node, List<Node> list)
            {
                foreach(var item in list)
                {
                    if (node.Location.x == item.Location.x && node.Location.y == item.Location.y)
                        return true;
                }
                return false;
            }

            private Node GetNodeWithLowestCost(Node[] nodes)
            {
                Node smallestCurrentNode = null;

                foreach (Node node in nodes)
                {
                    if(IsAjacentToDestination(node))
                    {
                        if (smallestCurrentNode == null)
                            smallestCurrentNode = node;

                        if (node.TotalCost < smallestCurrentNode.TotalCost)
                            smallestCurrentNode = node;

                    }
                }

                return smallestCurrentNode;
            }

            private void MarkObstaclesAsInaccessible(Rectangle[] obstacles)
            {
                foreach(var obstacle in obstacles)
                {
                    var startX = obstacle.x;
                    var startY = obstacle.y;
                    var endX = obstacle.x + obstacle.Width;
                    var endY = obstacle.y + obstacle.Height;


                    for (int i = startY; i <= endY; i++)
                    {
                        for (int ii = startX; ii <= endX; ii++)
                        {
                            if (_map[ii, i] == null)
                                _map[ii, i] = new Node(ii, i, _destination.x, _destination.y);

                            _map[ii, i].IsInaccessible = true;
                        }
                    }


                }
            }
    
#endregion

         }
}
