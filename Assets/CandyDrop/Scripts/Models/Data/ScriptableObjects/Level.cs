using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [Header("Номер уровня")]
    [SerializeField] private int _numberLevel;
    [Header("Длительность уровня(кол-во платформ)")]
    [SerializeField] private int _countFrameGame;
    [Header("Время на прохождение уровня(в мин.)")]
    [SerializeField] private int _timeToPass;
    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _ground;
    [SerializeField] private List<ContainerEnemyView> _enemies;
    [SerializeField] private List<BasePlatformView> _platforms;
    [SerializeField] private WallView _wall;
    [SerializeField] private HealthView _health;

    public int NumberLevel => _numberLevel;
    public int CountFrameGame => _countFrameGame;
    public int TimeToPass => _timeToPass;
    public Sprite Background => _background;
    public Sprite Ground => _ground;
    public List<ContainerEnemyView> Enemies => _enemies;
    public List<BasePlatformView> BasePlatforms => _platforms;
    public WallView Wall => _wall;
    public HealthView Health => _health;
}
