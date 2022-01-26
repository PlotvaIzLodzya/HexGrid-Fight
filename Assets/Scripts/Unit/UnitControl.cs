using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    [SerializeField] private Unit _currentUnit;
    [SerializeField] private MovementSystem _movementSystem;

    private Pointer _pointer;

    public Action MoveFinished;


    private void OnEnable()
    {
        _pointer = GetComponent<Pointer>();
        _pointer.HexSelected += HandleSelectedHex;
    }

    private void OnDisable()
    {
        _pointer.HexSelected -= HandleSelectedHex;
        _currentUnit.MovementFinished -= OnMoveFinished;
    }

    public void SetCurrentUnit(Unit currenUnit)
    {
        _currentUnit = currenUnit;
        _currentUnit.MovementFinished += OnMoveFinished;
        _movementSystem.ShowRange(_currentUnit);
    }

    private void HandleSelectedHex(Hex selectedHex)
    { 
        _movementSystem.MoveUnit(_currentUnit);
    }

    private void OnMoveFinished()
    {
        _movementSystem.HideRange();
        _movementSystem.ShowRange(_currentUnit);
        _movementSystem.ClearPath();
        MoveFinished?.Invoke();
    }
}
