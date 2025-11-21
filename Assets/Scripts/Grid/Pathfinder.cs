using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder
{
    static WorldGrid grid;
    static Dictionary<GridPosition, PathNode> nodes;

    public static void Initialize(WorldGrid worldGrid)
    {
        grid = worldGrid;
    }

    public static bool HasPath(GridPosition start, GridPosition end)
    {
        if(!grid.IsValidPosition(start) || !grid.IsValidPosition(end))
        {
            return false;
        }
        
        if(grid.GetCell(end).IsOccupied)
        {
            return false;
        }
        
        Queue<GridPosition> queue = new Queue<GridPosition>();
        HashSet<GridPosition> visited = new HashSet<GridPosition>();
        
        queue.Enqueue(start);
        visited.Add(start);
        
        while(queue.Count > 0)
        {
            GridPosition current = queue.Dequeue();
            
            if(current == end)
            {
                return true;
            }
            
            foreach(GridPosition neighborPos in current.CardinalNeighbors())
            {
                if(!grid.IsValidPosition(neighborPos))
                {
                    continue;
                }
                
                GridCell neighbor = grid.GetCell(neighborPos);
                if(!neighbor.IsWalkable || neighbor.IsOccupied || visited.Contains(neighborPos))
                {
                    continue;
                }
                
                visited.Add(neighborPos);
                queue.Enqueue(neighborPos);
            }
        }
        
        return false;
    }

    public static List<GridPosition> GetPath(GridPosition start, GridPosition end)
    {
        nodes = new Dictionary<GridPosition, PathNode>();
        Heap openSet = new Heap(grid.Width * grid.Height);
        HashSet<PathNode> closedSet = new HashSet<PathNode>();

        PathNode startNode = GetNode(start);
        startNode.SetG(0);
        startNode.SetH(GridPosition.GridDistance(start, end));
        openSet.Add(startNode);

        while(!openSet.IsEmpty)
        {
            PathNode current = openSet.Remove();
            if(current.Position == end)
            {
                return ReconstructPath(current);
            }

            closedSet.Add(current);
            foreach(GridPosition neighborPos in current.Position.CardinalNeighbors())
            {
                if(!grid.IsValidPosition(neighborPos))
                {
                    continue;
                }

                PathNode neighbor = GetNode(neighborPos);
                GridCell neighborCell = grid.GetCell(neighborPos);
                if(closedSet.Contains(neighbor) || !neighborCell.IsWalkable || neighborCell.IsOccupied)
                {
                    continue;
                }

                int tempG = current.G + 1;
                if(tempG < neighbor.G)
                {
                    neighbor.SetParent(current);
                    neighbor.SetG(tempG);
                    neighbor.SetH(GridPosition.GridDistance(neighborPos, end));

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    static PathNode GetNode(GridPosition position)
    {
        if(!nodes.TryGetValue(position, out PathNode node))
        {
            node = new PathNode(position);
            nodes.Add(position, node);
        }

        return node;
    }

    static List<GridPosition> ReconstructPath(PathNode end)
    {
        List<GridPosition> path = new List<GridPosition>();
        PathNode current = end;
        
        while(current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }
        
        path.Reverse();
        return path;
    }
}
