using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> _hexTiles = new Dictionary<Vector3Int, Hex>();
    private Dictionary<Vector3Int, List<Vector3Int>> _hexTileNeighbours = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach (var hex in FindObjectsOfType<Hex>())
        {
            _hexTiles[hex.HexCoordinates] = hex;
        }
    }

    public Hex GetHexAt(Vector3Int hexCoordinate)
    {
        Hex hex = null;

        _hexTiles.TryGetValue(hexCoordinate, out hex);

        return hex;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (_hexTiles.ContainsKey(hexCoordinates) == false)
            return new List<Vector3Int>();

        if (_hexTileNeighbours.ContainsKey(hexCoordinates))
            return _hexTileNeighbours[hexCoordinates];

        _hexTileNeighbours.Add(hexCoordinates, new List<Vector3Int>());

        foreach (var direcation in Direcation.GetDirecationList(hexCoordinates.z))
        {
            if (_hexTiles.ContainsKey(hexCoordinates + direcation))
            {
                _hexTileNeighbours[hexCoordinates].Add(hexCoordinates + direcation);
            }
        }

        return _hexTileNeighbours[hexCoordinates];
    }

    public Vector3Int GetClosestHex(Vector3 worldPosition)
    {
        worldPosition.y = 0;
        return HexCoordinates.ConvertPositionToOffset(worldPosition);
    }
}

public static class Direcation
{
    public static List<Vector3Int> DirectionsOffSetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1,0,1),
        new Vector3Int(0,0,1),
        new Vector3Int(1,0,0),
        new Vector3Int(0,0,-1),
        new Vector3Int(-1,0,-1),
        new Vector3Int(-1,0,0)
    };

    public static List<Vector3Int> DirectionOffSetEven = new List<Vector3Int>
    {
        new Vector3Int(0,0,1),
        new Vector3Int(1,0,1),
        new Vector3Int(1,0,0),
        new Vector3Int(1,0,-1),
        new Vector3Int(0,0,-1),
        new Vector3Int(-1,0,0)
    };

    public static List<Vector3Int> GetDirecationList(int zCoordinate) => zCoordinate % 2 == 0 ? DirectionOffSetEven : DirectionsOffSetOdd;
}
