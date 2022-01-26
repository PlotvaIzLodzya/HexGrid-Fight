using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UnitQueue : MonoBehaviour
{
    [SerializeField] private Turn _turn;
    [SerializeField] private UnitControl _unitControl;

    private List<Unit> _units;

    public Action UnitsReseted;

    private void Awake()
    {
        _units = FindObjectsOfType<Unit>().OrderByDescending(unit => unit.Initiative).ToList();
    }

    private void OnEnable()
    {
        _unitControl.MoveFinished += NextUnit;
    }

    private void OnDisable()
    {
        _unitControl.MoveFinished -= NextUnit;
    }

    private void Start()
    {
        NextUnit();
    }

    public void Sort()
    {
        List<Unit> units = _units.OrderByDescending(unit => unit.Initiative).ToList();
        _units = units;
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        Sort();
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public void NextUnit()
    {
        Unit unit = _units.FirstOrDefault(unit => unit.IsWaitingForTurn);


        if (unit != null)
        {
            unit.IsWaitingForTurn = false;
            _unitControl.SetCurrentUnit(unit);
        }
        else
        {
            ResetUnitsTurns();
        }


    }

    public void ResetUnitsTurns()
    {
        foreach (var unit in _units)
        {
            unit.IsWaitingForTurn = true;
        }

        UnitsReseted?.Invoke();
        NextUnit();
    }
}
