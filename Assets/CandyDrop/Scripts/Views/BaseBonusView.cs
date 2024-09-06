using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBonusView : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _positionBonusView;

    [SerializeField] private float _maxDistanceDelta;

    [HideInInspector]
    public UnityEvent UpdateBonusEventHandler = new UnityEvent();

    public void SetPositionBonusView(Vector3 position) =>
        _positionBonusView = position;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Vector3.Distance(_transform.position, _positionBonusView) > _maxDistanceDelta)
            _transform.position = Vector3.MoveTowards(_transform.position, _positionBonusView, _maxDistanceDelta);
        else
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        UpdateBonusEventHandler?.Invoke();
        UpdateBonusEventHandler.RemoveAllListeners();
    }
}
