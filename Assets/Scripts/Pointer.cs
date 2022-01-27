using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pointer : MonoBehaviour
{
    private Ray _ray;
    private Hex _lastHex;
    private Camera _camera;
    private PlayerInput _playerInput;

    public Action<Hex> HexSelected;
    public Action<Unit> UnitSelected;
    public Action<Vector3Int> OnHexHover;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.Clicked += HandleClick;
    }

    private void OnDisable()
    {
        _playerInput.Clicked -= HandleClick;
    }

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out RaycastHit _hitInfo))
        {
            if (_hitInfo.collider.TryGetComponent(out Hex hex) && hex.Reachable)
            {
                if(_lastHex != hex)
                {
                    OnHexHover?.Invoke(hex.HexCoordinates);
                    _lastHex = hex;
                }
            }
        }
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject target;

        if(FindTarget(mousePosition, out target))
        {
            if(target.TryGetComponent(out Hex hex) && hex.Reachable)
            {
                HexSelected?.Invoke(hex);
            } 
            else if(target.TryGetComponent(out Unit unit))
            {
                UnitSelected?.Invoke(unit);
            }
        }
    }


    public bool FindTarget(Vector3 mousePosition, out GameObject target)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            target = hitInfo.collider.gameObject;
            return true;
        }

        target = null;
        return false;
    }
}
