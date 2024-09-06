using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class GameManager : MonoBehaviour
{
    private Player _player;

    [SerializeField] private LevelView _levelView;
    [SerializeField] private BonusView _bonusView;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private WinView _winView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private TextMeshProUGUI _helthView;

    private LevelController _levelController;
    private BonusesController _bonusesController;
    private PlayerController _playerController;

    private void Start()
    {
        _levelController = new LevelController(_levelView);
        _levelController.SetPlayer(_playerView.transform);
        _levelController.Initialize();

        _bonusesController = new BonusesController(_bonusView);

        _player = new Player(_playerView.transform.position);
        _playerController = new PlayerController(_player, _playerView);
        _playerController.Initialize();

        _playerView.OnUpdateHealthCommand.Subscribe(_ => { _helthView.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Health.ToString(); });
        _playerView.OnUpdateHealthCommand.Execute();
        _levelView.Map.SystemInputPlayer.SubscribeButtomRight(() =>
        {
            if (_playerView.CollisionWallRight == false)
            {
                _playerView.ResetCollisionWallLeft();
                _levelView.Map.SetTargetPosition(Vector2.left);
            }
        });
        _levelView.Map.SystemInputPlayer.SubscribeButtomLeft(() =>
        {
            if(_playerView.CollisionWallLeft == false)
            {
                _playerView.ResetCollisionWallRight();
                _levelView.Map.SetTargetPosition(Vector2.right);
            }
        });
        _pauseView.OnPauseCommand.Subscribe(value =>
        {
            if (value)
                _playerView.PauseMove();
            else
                _playerView.ContinueMove();
        });
        _pauseView.OnPauseCommand.Subscribe(value => { _levelController.SetPause(value); });
        _playerView.OnDestroyCommand.Subscribe(_ => { _playerController.AddObjectsDisposable(); });
        _playerView.OnKilledEnemyCommand.Subscribe(_ => { _bonusesController.OnEnemyDeath(_playerView.transform.parent, _playerView.Position); });
        _playerView.OnWinCommand.Subscribe(_ => { _winView.SetActive(true); });
        //_playerView.OnKilledEnemyCommand.Subscribe(_ => { if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn == "1") Handheld.Vibrate(); });
        _playerView.OnGameOverCommand.Subscribe(_ =>
        {
            _gameOverView.SetActive(true);
            _playerView.PauseMove();
        });
        _playerView.OnGetLollipopCommand.Subscribe(_ => { if(_levelView.IsTimerEnd() == false) _bonusesController.OnFinishLevel(_playerView.transform.parent, _playerView.Position); });
    }

    private void FixedUpdate()
    {
        _playerController.BaseFixedUpdate();
        _levelController.BaseFixedUpdate();
    }
}
