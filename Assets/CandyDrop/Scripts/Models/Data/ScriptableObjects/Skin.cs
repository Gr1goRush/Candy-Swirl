using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObject/Skin")]
public class Skin : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private Sprite _skin;
    [SerializeField] private Sprite _face;

    public string Name => _name;
    public PlayerView Player => _playerView;
    public Sprite SkinImage => _skin;
    public Sprite Face => _face;
}
