using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(HexCoordinates))]
public class Hex : MonoBehaviour
{
    public Action Activated;
    public Action Deactivated;
    public bool IsOccupied;
    public int Cost = 1;

    private HexCoordinates _hexCoordinates;

    public Vector3Int HexCoordinates => _hexCoordinates.GetPosition();

    public bool Reachable { get; private set; }

    private void Awake()
    {
        _hexCoordinates = GetComponent<HexCoordinates>();
    }

    public void SetActive()
    {
        Reachable = true;

        if (IsOccupied == false)
            Activated?.Invoke();
    }

    public void SetDeactive()
    {
        Reachable = false;
        Deactivated?.Invoke();
    }
}

public enum HexType
{
    None,
    Default,
    Obstacle
}
