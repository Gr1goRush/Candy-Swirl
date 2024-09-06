using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController
{
    private Player _player;
    private PlayerView _playerView;

    public PlayerController(Player player, PlayerView playerView)
    {
        _player = player;
        _playerView = playerView;
    }

    public void Initialize()
    {
        _playerView.LoaidngSkin();
        _playerView.MinDistancePositionTraget.Subscribe(value => { _player.SetMinDistancePositionTraget(value); });
        _playerView.ForceJump.Subscribe(value => { _player.SetForceJump(value); });
        _playerView.SensitivityMove.Subscribe(value => { _player.SetSensitivityMove(value); });
        _playerView.OnCollisionEnterCommand.Subscribe(_ => { _player.Jump(); });
        _player.Position.Subscribe(position => { _playerView.UpdatePosition(position); });
        _player.ForceMove.Subscribe(value => { _playerView.UpdateVelocity(value); });
    }

    public void BaseFixedUpdate()
    {
        _player.Move();
    }

    public void AddObjectsDisposable()
    {
        ManagerUniRx.AddObjectDisposable<Vector2>(_player.Position);
        ManagerUniRx.AddObjectDisposable<Vector2>(_player.ForceMove);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.MinDistancePositionTraget);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.ForceJump);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.SensitivityMove);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnCollisionEnterCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnKilledEnemyCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnWinCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnGameOverCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnGetLollipopCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnUpdateHealthCommand);
        ManagerUniRx.AddObjectDisposable<Vector2>(_playerView.OnDestroyCommand);
    }

    public void OnDestroy()
    {
        ManagerUniRx.Dispose(_player.Position);
        ManagerUniRx.Dispose(_player.ForceMove);
        ManagerUniRx.Dispose(_playerView.MinDistancePositionTraget);
        ManagerUniRx.Dispose(_playerView.ForceJump);
        ManagerUniRx.Dispose(_playerView.SensitivityMove);
        ManagerUniRx.Dispose(_playerView.OnCollisionEnterCommand);
        ManagerUniRx.Dispose(_playerView.OnKilledEnemyCommand);
        ManagerUniRx.Dispose(_playerView.OnWinCommand);
        ManagerUniRx.Dispose(_playerView.OnGameOverCommand);
        ManagerUniRx.Dispose(_playerView.OnGetLollipopCommand);
        ManagerUniRx.Dispose(_playerView.OnUpdateHealthCommand);
        ManagerUniRx.Dispose(_playerView.OnDestroyCommand);
    }
}
