using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    [SerializeField] private Vector3Int _offsetCoordinates;

    internal Vector3Int GetPosition() => _offsetCoordinates;

    public const float XOffset = 1;
    public const float YOffset = 1;
    public const float ZOffset = 0.86f;


    private void OnValidate()
    {
        _offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    public static Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / XOffset);
        int y = Mathf.CeilToInt(position.y / YOffset);
        int z = Mathf.CeilToInt(position.z / ZOffset);

        return new Vector3Int(x, y, z);
    }
}
