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
    public Action UnitAdded;
    public Action UnitRemoved;
    public Action<int> UnitsOnSideAreOver;
    public Action AllUnitsAreOver;

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

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        Sort();
        UnitAdded?.Invoke();
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
        UnitRemoved?.Invoke();
        CheckIsAnyUnitsLeft(unit.Side);
    }

    private void CheckIsAnyUnitsLeft(int side)
    {
        List<Unit> sideUnits = _units.FindAll(unit => unit.Side == side);

        if (_units.Count <= 0)
            AllUnitsAreOver?.Invoke();
        else if (sideUnits.Count <= 0)
            UnitsOnSideAreOver?.Invoke(side);
    }

    private void NextUnit()
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

    private void ResetUnitsTurns()
    {
        foreach (var unit in _units)
        {
            unit.IsWaitingForTurn = true;
        }

        UnitsReseted?.Invoke();
        NextUnit();
    }

    private void Sort()
    {
        _units = _units.OrderByDescending(unit => unit.Initiative).ToList();
    }
}
