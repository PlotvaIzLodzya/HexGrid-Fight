using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(UnitQueue))]
public class Turn : MonoBehaviour
{
    [SerializeField] private UnitControl _unitControl;


    public void Begin(Unit unit)
    {
        _unitControl.SetCurrentUnit(unit);
    }
}
