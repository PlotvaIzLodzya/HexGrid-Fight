using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultDecieder : MonoBehaviour
{
    [SerializeField] private UnitQueue _unitQueue;

    private void OnEnable()
    {
        _unitQueue.UnitsOnSideAreOver += DecideWiner;
        _unitQueue.AllUnitsAreOver += Draw;
    }

    private void OnDisable()
    {
        _unitQueue.UnitsOnSideAreOver -= DecideWiner;
        _unitQueue.AllUnitsAreOver -= Draw;
    }

    public void DecideWiner(int side)
    {

    }

    public void Draw()
    {

    }
}
