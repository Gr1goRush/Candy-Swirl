using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelView : MonoBehaviour
{
    private float _timeLevelToPass;

    [SerializeField] private Image _background;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private FrameGameView _frameGamePrefab;
    [SerializeField] private MapView _map;
    [SerializeField] private AnimalFinishView _animalFinishPrefab;
    [SerializeField] private TextMeshProUGUI _timerLevelToPassView;

    public List<Level> Levels => _levels;
    public FrameGameView FrameGamePrefab => _frameGamePrefab;
    public MapView Map => _map;
    public AnimalFinishView AnimalFinish => _animalFinishPrefab;

    public void SetTimeLevelToPass(float value) =>
        _timeLevelToPass = value * 60;

    public void SetBackground(Sprite spriteBackground) =>
        _background.sprite = spriteBackground;

    public bool IsTimerEnd() =>
        _timeLevelToPass == 0;

    public void UpdateTimerText()
    {
        _timeLevelToPass -= Time.deltaTime;

        if (_timeLevelToPass < 0)
            _timeLevelToPass = 0;

        float minutes = Mathf.FloorToInt(_timeLevelToPass / 60);
        float seconds = Mathf.FloorToInt(_timeLevelToPass % 60);
        _timerLevelToPassView.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
