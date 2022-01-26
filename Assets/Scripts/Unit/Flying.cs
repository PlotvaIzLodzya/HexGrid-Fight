using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : Movable
{ 
    public void MoveTo(Vector3 Destination)
    {
        StartCoroutine(Fly(Destination));
    }

    private IEnumerator Fly(Vector3 Destination)
    {
        while(transform.position != Destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, 0.5f);

            yield return null;
        }

        Moved?.Invoke();
    }
}
