using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int HealthPoint;
    public int Speed;
    public int Damage;
    public int Initiative;
    public int UnitAmount;
    public int Side;
    public bool IsFlying;
    public bool IsWaitingForTurn = true;
    private float _movementDuration = 0.2f;

    private Queue<Vector3> _pathPositions = new Queue<Vector3>();

    public Action MovementFinished;
    public Action<Unit> UnitDied;

    public void MoveThroughPath(List<Vector3> currentPath)
    {
        _pathPositions = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = _pathPositions.Dequeue();
        StartCoroutine(MovementCoroutine(firstTarget));
    }

    public void Attack(List<Vector3> currentPath)
    {
        _pathPositions = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = _pathPositions.Dequeue();
        StartCoroutine(MovementCoroutine(firstTarget));
    }

    private IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        Vector3 startPosition = transform.position;
        endPosition.y = startPosition.y;
        float timeElapsed = 0;

        while (timeElapsed< _movementDuration)
        {
            timeElapsed += Time.deltaTime;
            float lerpStep = timeElapsed / _movementDuration;

            transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
            yield return null;
        }

        transform.position = endPosition;

        if (_pathPositions.Count > 0)
        {
            StartCoroutine(MovementCoroutine(_pathPositions.Dequeue()));
        }
        else
        {
            MovementFinished?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {
            HealthPoint = 0;
            UnitDied?.Invoke(this);
        }
    }

    public void ApplyDamage(Unit unit)
    {
        unit.TakeDamage(Damage);
    }
}
