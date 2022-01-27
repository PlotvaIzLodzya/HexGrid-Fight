using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphSearch
{    
    public static BFSResult BFSGetRange(HexGrid hexGrid, Vector3Int startPoint, int speed, bool isObstacleIgnored)
    {
        Dictionary<Vector3Int, Vector3Int?> visitedHex = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costHistory = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> hexToVisit = new Queue<Vector3Int>();
        
        hexToVisit.Enqueue(startPoint);
        costHistory.Add(startPoint, 0);
        visitedHex.Add(startPoint, null);

        while (hexToVisit.Count>0)
        {
            Vector3Int currentHex = hexToVisit.Dequeue();

            foreach (var neighbourPosition in hexGrid.GetNeighboursFor(currentHex))
            {
                if (hexGrid.GetHexAt(neighbourPosition).IsOccupied && isObstacleIgnored == false)
                    continue;

                int hexCost = hexGrid.GetHexAt(neighbourPosition).Cost;
                int currentCost = costHistory[currentHex];
                int newCost = currentCost + hexCost;

                if(newCost <= speed)
                {
                    if(visitedHex.ContainsKey(neighbourPosition) == false)
                    {
                        visitedHex[neighbourPosition] = currentHex;
                        costHistory[neighbourPosition] = newCost;
                        hexToVisit.Enqueue(neighbourPosition);
                    }
                    else if(costHistory[neighbourPosition] > newCost)
                    {
                        costHistory[neighbourPosition] = newCost;
                        visitedHex[neighbourPosition] = currentHex;
                    }
                }
            }
        }

        return new BFSResult { VisitedHex = visitedHex };
    }

    public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedHex)
    {
        List<Vector3Int> path = new List<Vector3Int>();

        path.Add(current);
        
        while(visitedHex[current] != null)
        {
            path.Add(visitedHex[current].Value);
            current = visitedHex[current].Value;
        }

        path.Reverse();

        return path.Skip(1).ToList();
    }
}

public struct BFSResult
{
    public Dictionary<Vector3Int, Vector3Int?> VisitedHex;

    public List<Vector3Int> GetPathTo(Vector3Int destination)
    {
        if (VisitedHex.ContainsKey(destination) == false)
            return new List<Vector3Int>();

        return GraphSearch.GeneratePathBFS(destination, VisitedHex);
    }

    public bool IsHexPositionInRange(Vector3Int position)
    {
        return VisitedHex.ContainsKey(position);
    }

    public IEnumerable<Vector3Int> GetRangePosition() => VisitedHex.Keys;
}
