using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformView : MonoBehaviour
{
    [SerializeField] private PlatformView _platformView;

    public PlatformView Platform => _platformView;

    private Transform _transform;
    private Transform _player;

    [SerializeField] private float _minDistanceToPlayer = 10;
    [SerializeField] private float _timeHide = 4;
    [SerializeField] private TypePlatform _typePlatform;

    public void SetTypePlatform(int indexTypePlatform) =>
        _typePlatform = indexTypePlatform == 2 ? TypePlatform.Hide : TypePlatform.NoHide;

    public void SetLocalPosition(Vector3 position) =>
        transform.localPosition = position;

    public void SetPlayer(Transform player) =>
        _player = player;

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_transform.position, _player.position) < _minDistanceToPlayer)
        {
            _transform.position += Vector3.up * Time.deltaTime;

            if (_typePlatform == TypePlatform.Hide)
                StartCoroutine(StartHide());
        }
    }

    private IEnumerator StartHide()
    {
        yield return new WaitForSeconds(_timeHide);
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_platformView == null)
            _platformView = transform.GetChild(0).GetComponent<PlatformView>();
    }
}
