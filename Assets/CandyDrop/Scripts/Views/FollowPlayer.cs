using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _transform;
    private float _yPosition;
    private float _xDifference;

    [SerializeField] private PlayerView _playerView;

    private void Start()
    {
        _transform = transform;
        _yPosition = _transform.position.y;

        if(_playerView != null)
            _xDifference = _transform.position.x - _playerView.Position.x;
    }

    private void Update()
    {
        if(_playerView != null)
            _transform.position = new Vector3(_xDifference + _playerView.transform.position.x, _yPosition, _playerView.transform.position.z);
        else
            _transform.position = new Vector3(_transform.position.x, _yPosition, _transform.position.z);
    }
}
