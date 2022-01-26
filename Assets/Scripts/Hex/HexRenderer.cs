using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexRenderer : MonoBehaviour
{
    [SerializeField] private Material _defautMaterial;
    [SerializeField] private Material _highlitedMaterial;
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _pathMaterial;

    private Hex _hex;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _hex = GetComponent<Hex>();
    }

    private void OnEnable()
    {
        _hex.Activated += HighlightReachableHex;
        _hex.Deactivated += ResetMaterial;
    }

    private void OnDisable()
    {
        _hex.Activated -= HighlightReachableHex;
        _hex.Deactivated -= ResetMaterial;
    }

    public void HighlightReachableHex()
    {
        _meshRenderer.material = _activeMaterial;
    }

    public void HighlihtPath()
    {
        _meshRenderer.material = _pathMaterial;
    }

    public void ResetHighlightPath()
    {
        _meshRenderer.material = _activeMaterial;
    }

    public void ResetMaterial()
    {
        _meshRenderer.material = _defautMaterial;
    }
}
