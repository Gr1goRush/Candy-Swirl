using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player
{
    private float _sensitivityMove;
    private float _minDistancePositionTraget;

    public Player(Vector2 startPosition)
    {
        Position.Value = startPosition;
    }

    public ReactiveProperty<Vector2> Position { get; private set; } = new();
    public ReactiveProperty<Vector2> ForceMove { get; private set; } = new();
    public float ForceJump { get; private set; }

    public void SetSensitivityMove(float value) =>
        _sensitivityMove = value;

    public void SetMinDistancePositionTraget(float value) =>
        _minDistancePositionTraget = value;

    public void SetForceJump(float value) =>
        ForceJump = value;

    public void Jump() =>
        ForceMove.Value = Vector2.up * ForceJump;

    public void Move()
    {
        if (Vector2.Distance(Vector2.zero, ForceMove.Value) > _minDistancePositionTraget)
        {
            var lerpForce = Vector2.Lerp(Vector2.zero, ForceMove.Value, _sensitivityMove);
            ForceMove.Value -= lerpForce;
        }
        else
            ForceMove.Value = Vector2.zero;
    }
}
