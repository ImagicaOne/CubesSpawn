using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinder
{
    public static List<Tile> FindPath(Tile[,] tiles, Tile start, Tile goal)
    {
        // Шаг 1.
        var closedSet = new List<PathNode>();
        var openSet = new List<PathNode>();
        // Шаг 2.
        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = 0,
            HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
        };
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            // Шаг 3.
            var currentNode = openSet.OrderBy(node =>
              node.EstimateFullPathLength).First();
            // Шаг 4.
            if (currentNode.Position == goal)
                return GetPathForNode(currentNode);
            // Шаг 5.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // Шаг 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, tiles))
            {
                // Шаг 7.
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                  node.Position == neighbourNode.Position);
                // Шаг 8.
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else
                  if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // Шаг 9.
                    openNode.CameFrom = currentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }
        // Шаг 10.
        return null;
    }

    private static int GetDistanceBetweenNeighbours()
    {
        return 1;
    }

    private static int GetHeuristicPathLength(Tile from, Tile to)
    {
        //return (int)Vector3.Distance(from.transform.position, to.transform.position);
        return (int)Mathf.Abs(from.transform.position.x - to.transform.position.x) + (int)Mathf.Abs(from.transform.position.z - to.transform.position.z);
    }

    private static List<PathNode> GetNeighbours(PathNode pathNode, Tile goal, Tile[,] tiles)
    {
        var result = new List<PathNode>();

        // Соседними точками являются соседние по стороне клетки.
        Tile[] neighbourPoints = new Tile[4];

        if (pathNode.Position.placeInMap.x + 1 < tiles.GetLength(0))
        {
            neighbourPoints[0] = tiles[pathNode.Position.placeInMap.x + 1, pathNode.Position.placeInMap.y];
        }

        if (pathNode.Position.placeInMap.x - 1 >= 0)
        {
            neighbourPoints[1] = tiles[pathNode.Position.placeInMap.x - 1, pathNode.Position.placeInMap.y];
        }

        if (pathNode.Position.placeInMap.y + 1 < tiles.GetLength(1))
        {
            neighbourPoints[2] = tiles[pathNode.Position.placeInMap.x, pathNode.Position.placeInMap.y + 1];
        }

        if (pathNode.Position.placeInMap.y - 1 >= 0)
        {
            neighbourPoints[3] = tiles[pathNode.Position.placeInMap.x, pathNode.Position.placeInMap.y - 1];
        }

        foreach (var point in neighbourPoints)
        {         
            if (point is null || point.isObstacle )
            {
                continue;
            }

            // Заполняем данные для точки маршрута.
            var neighbourNode = new PathNode()
            {
                Position = point,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(),
                HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
            };
            result.Add(neighbourNode);
        }
        return result;
    }

    private static List<Tile> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Tile>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }
}
