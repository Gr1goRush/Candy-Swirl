using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _targetPosition;

    [SerializeField] private SystemInputPlayer _systemInputPlayer;
    [SerializeField] private float _sensitivityMove;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _minDistanceTarget;

    public SystemInputPlayer SystemInputPlayer;

    //public void SetTargetPosition(Vector2 targetMove) =>
    //    _targetPosition = ((Vector2)_transform.position + targetMove) * _speedMove;

    public void SetTargetPosition(Vector2 targetMove) =>
        _transform.position += new Vector3(targetMove.x * Time.deltaTime * _speedMove, 0, 0);

    public void Move()
    {
        //if (Vector2.Distance(_transform.position, _targetPosition) > _minDistanceTarget)
        //{
        //    var positionLerp = Vector2.Lerp(_transform.position, _targetPosition, _sensitivityMove);
        //    _transform.position = new Vector2(positionLerp.x, _transform.position.y);
        //}
    }

    private void Start()
    {
        _transform = transform;
    }

    private void OnValidate()
    {
        if (_systemInputPlayer == null)
            _systemInputPlayer = GetComponent<SystemInputPlayer>();
    }
}
