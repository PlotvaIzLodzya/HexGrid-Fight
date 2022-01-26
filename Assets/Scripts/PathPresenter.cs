using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPresenter : MonoBehaviour
{
    [SerializeField] private Pointer _pointer;
    [SerializeField] private MovementSystem _movementSystem;

    private void OnEnable()
    {
        _pointer.OnHexHover += Highlight;
    }

    private void OnDisable()
    {
        _pointer.OnHexHover -= Highlight;
    }

    private void Highlight(Vector3Int destinationHexCoordinates)
    {
        _movementSystem.ShowPath(destinationHexCoordinates);
    }
}
