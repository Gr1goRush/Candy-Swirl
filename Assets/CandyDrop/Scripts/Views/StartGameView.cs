using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameView : MonoBehaviour
{
    [SerializeField] private Button _startGame;

    private void Start()
    {
        _startGame.onClick.AddListener(() => { ManagerScenes.Instance.LoadAsyncFromCoroutine("Game"); });
    }

    private void OnValidate()
    {
        if (_startGame == null)
            _startGame = GetComponent<Button>();
    }
}
