using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    private LevelView _levelView;
    private bool _isPauseOn;
    private Transform _player;

    public LevelController(LevelView levelView)
    {
        _levelView = levelView;
        _isPauseOn = false;
    }

    public void Initialize()
    {
        LoadDataLevel();
    }

    public void SetPlayer(Transform player) =>
        _player = player;

    public void SetPause(bool value)
    {
        if (value)
            AudioManager.Instance.PauseSourceSound();
        else
            AudioManager.Instance.PlaySourceSound();

        _isPauseOn = value;
    }

    public void BaseFixedUpdate()
    {
        if (_isPauseOn == false)
        {
            _levelView.Map.Move();
            _levelView.UpdateTimerText();
        }
    }

    public void LoadDataLevel()
    {
        if (_levelView.Levels.Count == 0)
        {
            Debug.Log("Список уровней пустой!");
            return;
        }

        var loadedLevel = _levelView.Levels.Find(level => level.NumberLevel == ContainerSaveerPlayerPrefs.Instance.SaveerData.Level);
        _levelView.SetBackground(loadedLevel.Background);

        var indexFrameToHealth = Random.Range(0, loadedLevel.CountFrameGame);

        if (loadedLevel == null)
            loadedLevel = _levelView.Levels[0];

        for (int i = 0; i < loadedLevel.CountFrameGame; i++)
        {
            var frameGame = Object.Instantiate(_levelView.FrameGamePrefab, _levelView.Map.transform);
            frameGame.SetPositionBottom(i * frameGame.Width);
            frameGame.SetGround(loadedLevel.Ground);

            if (i == indexFrameToHealth)
            {
                var health = Object.Instantiate(loadedLevel.Health, frameGame.transform);
                health.SetLocalPosition(new Vector3(0, frameGame.Height * 2, 0));
            }

            if (i == 1)
            {
                var wall = Object.Instantiate(loadedLevel.Wall, _levelView.Map.transform);
                wall.SetLocalPosition(new Vector3(-frameGame.Width / 2, 0, 0));
                wall.SetTypeWall(TypeWall.Left);
            }

            if (i % 2 != 0)
                frameGame.FlipHorizontally();

            if (i != 0 && i != loadedLevel.CountFrameGame - 2 && i != loadedLevel.CountFrameGame - 1)
            {
                var quantityEnemyToFrame = Random.Range(1, loadedLevel.NumberLevel);
                var xEnemy = frameGame.transform.position.x;
                var yEnemy = Random.Range(frameGame.Height / 2, frameGame.Height);
                var positionOffcet = 50f;

                for (int j = 0; j < quantityEnemyToFrame; j++)
                {
                    var indexEnemy = Random.Range(0, loadedLevel.Enemies.Count);
                    var enemy = Object.Instantiate(loadedLevel.Enemies[indexEnemy], frameGame.transform);
                    enemy.SetLocalPosition(new Vector3(xEnemy + j * positionOffcet, yEnemy, 0));
                }
            }

            var x = Random.Range(frameGame.transform.position.x - frameGame.Width / 4, frameGame.transform.position.x + frameGame.Width / 4);
            var y = Random.Range(frameGame.transform.position.y, frameGame.transform.position.y + frameGame.Height / 2);

            if (i == loadedLevel.CountFrameGame - 1)
            {
                var wall = Object.Instantiate(loadedLevel.Wall, frameGame.transform);
                wall.SetLocalPosition(new Vector3(0, 0, 0));
                wall.SetTypeWall(TypeWall.Right);
            }

            if (i == loadedLevel.CountFrameGame - 2)
            {
                var animalFinish =  Object.Instantiate(_levelView.AnimalFinish, frameGame.transform);
                animalFinish.SetLocalPosition(new Vector3(0, frameGame.Height, 0));
            }
            else
            {
                if (i != loadedLevel.CountFrameGame - 1)
                {
                    var indexPlatform = Random.Range(0, loadedLevel.BasePlatforms.Count);
                    var platform = Object.Instantiate(loadedLevel.BasePlatforms[indexPlatform], frameGame.transform);
                    platform.SetLocalPosition(new Vector3(x, y, 0));
                    platform.SetPlayer(_player);
                    platform.SetTypePlatform(Random.Range(0, 3));
                }
            }
        }

        _levelView.SetTimeLevelToPass(loadedLevel.TimeToPass);
    }
}
