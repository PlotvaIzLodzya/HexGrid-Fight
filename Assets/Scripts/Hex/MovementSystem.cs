using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private HexGrid _hexGrid;

    private BFSResult _range = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();

    public void HideRange()
    {
        foreach (var hexPosition in _range.GetRangePosition())
        {
            _hexGrid.GetHexAt(hexPosition).SetDeactive();
        }
    }

    public void ShowRange(Unit selectedUnit)
    {
        CalculateRange(selectedUnit);

        foreach (var hexPosition in _range.GetRangePosition())
        {
            _hexGrid.GetHexAt(hexPosition).SetActive();
        }
    }

    public void ShowPath(Vector3Int destinationHexCoordinates)
    {
        if (_range.GetRangePosition().Contains(destinationHexCoordinates))
        {
            foreach (var hexPosition in currentPath)
            {
                _hexGrid.GetHexAt(hexPosition).GetComponent<HexRenderer>().ResetHighlightPath();
            }

            currentPath = _range.GetPathTo(destinationHexCoordinates);

            foreach (var hexPosition in currentPath)
            {
                _hexGrid.GetHexAt(hexPosition).GetComponent<HexRenderer>().HighlihtPath();
            }
        }
    }

    public void MoveUnit(Unit selectedUnit)
    {
        selectedUnit.MoveThroughPath(currentPath.Select(pos=> _hexGrid.GetHexAt(pos).transform.position).ToList());
        HideRange();
    }

    public void AttackUnit(Unit selectedUnit)
    {
        selectedUnit.Attack(currentPath.Select(pos => _hexGrid.GetHexAt(pos).transform.position).ToList());
        HideRange();
    }

    public void ClearPath()
    {
        currentPath = new List<Vector3Int>();
    }

    private void CalculateRange(Unit selectedUnit)
    {
        _range = GraphSearch.BFSGetRange(_hexGrid, _hexGrid.GetClosestHex(selectedUnit.transform.position), selectedUnit.Speed, selectedUnit.IsFlying);
    }
}
