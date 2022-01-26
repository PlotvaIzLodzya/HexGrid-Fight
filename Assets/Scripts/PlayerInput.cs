using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Clicked?.Invoke(Input.mousePosition);
    }
}
