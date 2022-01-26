using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    private void Awake()
    {
        HealthPoint = 20;
        Speed = 5;
        Damage = 7;
    }
}
