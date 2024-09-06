using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallView : MonoBehaviour
{
    private TypeWall _typeWall;

    public TypeWall TypeWall => _typeWall;

    public void SetTypeWall(TypeWall typeWall) =>
        _typeWall = typeWall;

    public void SetLocalPosition(Vector3 position) =>
        transform.localPosition = position;
}
